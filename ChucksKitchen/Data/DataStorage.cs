namespace ChucksKitchen.Data
{
    public class DataStorage
    {
        public Dictionary<string, object> DbFile = [];
        /*
         *  Concurrency: If multiple threads add users/orders at the same time,
         *  you might get race conditions.
         *  In a real-world system, you’d often rely on a database auto-increment or GUIDs.

Deleted Records: If you delete a user with the highest ID, 
        the next ID will still increment from the max, not reuse the deleted one.
        This is usually fine, but worth noting.
         */
    }
}
