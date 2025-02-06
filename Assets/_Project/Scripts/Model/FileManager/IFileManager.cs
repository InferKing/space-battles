namespace _Project.Scripts.Model.FileManager
{
    public interface IFileManager
    {
        void Save<T>(string fileName, T data, string folderName = Constants.DefaultDataFolder);
        T Load<T>(string fileName, System.Func<T> defaultFactory, string folderName = Constants.DefaultDataFolder) where T : class;
    }
}