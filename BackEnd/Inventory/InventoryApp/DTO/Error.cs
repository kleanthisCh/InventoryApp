namespace InventoryApp.DTO
{
    public class Error
    {
        public string? Code { get; set; }
        public string? Message { get; set; }
        public string? field { get; set; }
        public Error(string code, string message, string field) 
        {
            this.Code = code;
            this.Message = message;
            this.field = field;

        }
    }
}
