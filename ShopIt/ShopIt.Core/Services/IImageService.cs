using System;
using System.Threading.Tasks;

namespace ShopIt.Core.Services
{
	public interface IImageService
	{
		Task<byte[]> TakePicture();
		Task<byte[]> SelectFromLibrary();
		Task<string> SaveImage(string imageName, byte[] data);
		Task<bool> DeleteImage(string imageName);
		byte[] GetByteFromImage(string path);
	}
}

