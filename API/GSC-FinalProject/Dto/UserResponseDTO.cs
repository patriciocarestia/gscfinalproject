namespace GSC_FinalProject.Dto
{
    public class UserResponseDTO
    {
        public string Token { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
        public bool Success { get; set; }
    }
}
