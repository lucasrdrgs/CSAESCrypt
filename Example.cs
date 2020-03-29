using System;
using System.IO;
using CsAesCrypt;

namespace Example {
	class ExampleProgram {
		static void Main(string[] args) {
			// This will create a CsAesCrypt instance with randomly generated key & IV
			CsAesCrypt c = new CsAesCrypt();
			
			// If you want to use custom key and IV, pass them as arguments
			// to the constructor: ... = new CsAesCrypt(yourKey, yourIV);
			// PS: key must be 32 chars long and IV must be 16 chars long.

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
