namespace BE_RestaurantManagement.DTOs
{
    public class TableDTO
    {
        public int TableId { get; set; }
        public int Capacity { get; set; }
        public string Status { get; set; }
    }

    public class CreateTableDTO
    {
        public int Capacity { get; set; }
    }

    public class UpdateTableDTO
    {
        public int Capacity { get; set; }
        public string Status { get; set; }
    }
}
