﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using BCrypt.Net;

namespace Orion.Hashing
{
	public sealed class Hasher
	{
		private readonly Orion _core;

		public Hasher(Orion orion)
		{
			_core = orion;
		}

		/// <summary>
		/// Verifies that the given input matches the given hash. 
		/// If the given hash is not BCrypt, it is updated to be a BCrypt hash
		/// </summary>
		/// <param name="input">Unhashed string</param>
		/// <param name="hash">Hashed string</param>
		/// <param name="workFactor">BCrypt workfactor to use for hash upgrades</param>
		/// <returns>true if the input matches the hash</returns>
		public bool VerifyHash(string input, ref string hash, int workFactor)
		{
			try
			{
				if (BCrypt.Net.BCrypt.Verify(input, hash))
				{
					// If necessary, perform an upgrade to the highest work factor.
					UpgradeHashWorkFactor(input, ref hash, workFactor);
					return true;
				}
			}
			catch (SaltParseException)
			{
				if (String.Equals(HashString(input), hash, StringComparison.CurrentCulture))
				{
					UpgradeInputToBCrypt(input, ref hash, workFactor);
					return true;
				}
				return false;
			}
			return false;
		}

		/// <summary>Creates a BCrypt hash from a string and returns it</summary>
		/// <param name="text">The plain text string to hash</param>
		/// <param name="minLength">The minimum required length of the input text</param>
		/// <param name="workFactor">BCrypt work factor</param>
		public string CreateBCryptHash(string text, int minLength, int workFactor = 7)
		{
			string ret;

			if (text.Trim().Length < Math.Max(4, minLength))
			{
				throw new ArgumentOutOfRangeException(String.Format(Strings.BCryptHashLengthOutOfRange, minLength));
			}
			try
			{
				ret = BCrypt.Net.BCrypt.HashPassword(text.Trim(), workFactor);
			}
			catch (ArgumentOutOfRangeException)
			{
				_core.Log.ConsoleError(Strings.BCryptHashWorkFactorOutOfRange);
				ret = BCrypt.Net.BCrypt.HashPassword(text.Trim());
			}

			return ret;
		}

		/// <summary>
		/// Upgrades a given hash to BCrypt
		/// </summary>
		/// <param name="input">input text</param>
		/// <param name="hash">old hash to be updated</param>
		/// <param name="workFactor">BCrypt workfactor to be applied to the hash</param>
		private void UpgradeInputToBCrypt(string input, ref string hash, int workFactor)
		{
			try
			{
				hash = BCrypt.Net.BCrypt.HashPassword(input, workFactor);
			}
			catch (ArgumentOutOfRangeException)
			{
				_core.Log.ConsoleError(Strings.BCryptHashWorkFactorOutOfRangeUpgrade);
				hash = BCrypt.Net.BCrypt.HashPassword(input);
			}
		}

		/// <summary>Upgrades a password to the highest work factor available in the config.</summary>
		/// <param name="password">The raw user password (unhashed) to upgrade</param>
		/// <param name="hash">Currently hashed password to upgrade</param>
		/// <param name="workFactor">BCrypt workfactor to check against and update to</param>
		private void UpgradeHashWorkFactor(string password, ref string hash, int workFactor)
		{
			// If the destination work factor is not greater, we won't upgrade it or re-hash it
			int currentWorkFactor;
			try
			{
				currentWorkFactor = Int32.Parse((hash.Split('$')[2]));
			}
			catch (FormatException)
			{
				_core.Log.ConsoleError(Strings.BCryptInvalidHashFormat);
				return;
			}

			if (currentWorkFactor < workFactor)
			{
				try
				{
					hash = BCrypt.Net.BCrypt.HashPassword(password, workFactor);
				}
				catch (ArgumentOutOfRangeException)
				{
					_core.Log.ConsoleError(Strings.BCryptInvalidWorkFactor);
				}
			}
		}

		/// <summary>
		/// A dictionary of hashing algorithms and an implementation object.
		/// This is only here for backwards compatibility.
		/// </summary>
		private readonly Dictionary<string, Func<HashAlgorithm>> HashTypes = new Dictionary
			<string, Func<HashAlgorithm>>
		{
			{"sha512", () => new SHA512Managed()},
			{"sha256", () => new SHA256Managed()},
			{"md5", () => new MD5Cng()},
			{"sha512-xp", SHA512.Create},
			{"sha256-xp", SHA256.Create},
			{"md5-xp", MD5.Create}
		};

		/// <summary>
		/// Returns a hashed string for a given string based on the config file's hash algo.
		/// This is only here for backwards compatibility.
		/// </summary>
		/// <param name="bytes">bytes to hash</param>
		/// <returns>string hash</returns>
		private string HashString(byte[] bytes)
		{
			if (bytes == null)
				throw new NullReferenceException("bytes");
			Func<HashAlgorithm> func;
			if (!HashTypes.TryGetValue(_core.Config.HashAlgorithm.ToLower(), out func))
				throw new NotSupportedException(String.Format(Strings.HashAlgoUnsupported, _core.Config.HashAlgorithm.ToLower()));

			using (HashAlgorithm hash = func())
			{
				byte[] ret = hash.ComputeHash(bytes);
				return ret.Aggregate("", (s, b) => s + b.ToString("X2"));
			}
		}

		/// <summary>
		/// Returns a hashed string for a given string based on the config file's hash algo
		/// </summary>
		/// <param name="input">string to hash</param>
		/// <param name="hashed">current hashed password</param>
		/// <returns>string hash</returns>
		private string HashString(string input)
		{
			return HashString(Encoding.UTF8.GetBytes(input));
		}
	}
}