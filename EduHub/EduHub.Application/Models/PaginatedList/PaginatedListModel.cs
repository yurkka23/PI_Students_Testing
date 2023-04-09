
namespace EduHub.Application.Models.PaginatedList;
public class PaginatedListModel<T>
{
    public int PageNumber { get; private set; }
    public int TotalPages { get; private set; }
    public List<T> Items { get; set; }

    public PaginatedListModel(List<T> items, int count, int pageNumber, int pageSize)
    {
        PageNumber = pageNumber;
        TotalPages = (int)Math.Ceiling(count / (double)pageSize);
        Items = items;  
    }

    public bool HasPreviousPage
    {
        get
        {
            return (PageNumber > 1);
        }
    }

    public bool HasNextPage
    {
        get
        {
            return (PageNumber < TotalPages);
        }
    }
}