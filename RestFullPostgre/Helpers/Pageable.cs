namespace RestFullPostgre.Helpers
{
    public class Pageable<T> where T : class
    {
        public List<T>? data { get; set; }
        public int pageNumber { get; set; }
        public int pageSize { get; set; }
        public int totalPage { get; set; }
        public int totalCount { get; set; }

        public Pageable()
        {
        }

        public Pageable(List<T> _listData, int _pageNumber, int _pageSize, int _totalCount)
        {
            data = _listData;
            pageNumber = _pageNumber;
            pageSize = _pageSize;
            totalPage = (int) Math.Ceiling(_totalCount / (double) _pageSize);
            totalCount = _totalCount;
        }

        public Pageable<T> ToPageableList(List<T> source, int pageNumber, int pageSize)
        {
            var countSize = source.Count;
            var items = source.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();

            return new Pageable<T>(items, pageNumber, pageSize, countSize);
        }
    }
}
