using ChucksKitchen.Data;

namespace ChucksKitchen.Repositories
{
    public abstract class BaseRepo
    {
        private readonly DataStorage _dataStorage;

        protected BaseRepo(DataStorage dataStorage)
        {
            _dataStorage = dataStorage;

        }
        public List<T> GetDataFile<T>(string dbFileName)
        {
            if (_dataStorage?.DbFile == null)
                throw new InvalidOperationException("Database storage is not initialized.");

            if (!_dataStorage.DbFile.TryGetValue(dbFileName, out var documentFile) || documentFile == null)
            {
                // Initialize a new list if none exists
                var newList = new List<T>();
                _dataStorage.DbFile[dbFileName] = newList;
                return newList;
            }

            if (documentFile is List<T> typedList)
            {
                return typedList;
            }

            throw new InvalidCastException($"Stored object for {dbFileName} is not a List<{typeof(T).Name}>.");
        }


        //public List<T> GetDataFile<T>(string dbFileName)
        //{
        //    _dataStorage.DbFile.TryGetValue(dbFileName, out var documentFile);
        //    var fileData = documentFile is null ? [] : (List<T>)documentFile;
        //    return fileData;
        //}
    }
}
