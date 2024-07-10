using System;
namespace LibraryManagementSystem.FileContexts
{
	public interface IFileContext<T>
	{
        List<T> ReadFromFile(string filePath);
        void WriteToFile(string filePath, List<T> items);
    }
}

