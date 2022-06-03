namespace API_SAADS_CORE_61.DTOs
{
    public class TareaDTO
    {
        public long Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        public int IdInterface { get; set; }
        public int Tipo { get; set; }

    }
}
