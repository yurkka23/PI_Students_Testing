namespace EduHub.Application.Models.PaginatedList
{
    public class PaginatedListModel<T>
    {
        public PaginatedListModel(List<T> items, int count, int pageNumber, int pageSize)
        {
            PageNumber = pageNumber;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
            Items = items;
        }

        public int PageNumber { get; }
        public int TotalPages { get; }
        public List<T> Items { get; set; }

        public bool HasPreviousPage => PageNumber > 1;

        public bool HasNextPage => PageNumber < TotalPages;
    }
}