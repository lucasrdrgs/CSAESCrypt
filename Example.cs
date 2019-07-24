using System;
using System.IO;
using CsAesCrypt;

namespace Example
{
	class ExampleProgram
	{
		static void Main(string[] args)
		{
			// This will create a CsAesCrypt instance with randomly generated key & IV
			CsAesCrypt c = new CsAesCrypt();
			
			// If you want to use your own key & iv, do this:
			// CsAesCrypt c = new CsAesCrypt(yourKey, yourIV);
			// Please use a 32 char long key and a 16 char long IV.
			
			string path = Console.ReadLine();
			if(!File.Exists(path)) {
				throw new Exception("Invalid path!");
			}
			
			byte[] fileBytes = {};
			
			fileBytes = File.ReadAllBytes(path);
			
			// Encryption:
			byte[] encodedBytes = c.EncodeBytes(fileBytes);
			File.WriteAllBytes(path, encodedBytes);
			Console.WriteLine("Encrypted.");
			
			// Decryption:
			fileBytes = File.ReadAllBytes(path);
			byte[] decodedBytes = c.DecodeBytes(fileBytes);
			File.WriteAllBytes(path, decodedBytes);
			Console.WriteLine("Decrypted.");
			
			Console.WriteLine("Done.");
			Console.ReadKey();
		}
	}
}
