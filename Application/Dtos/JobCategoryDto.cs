namespace Application.Dtos
{
    public class JobCategoryDto
    {
        public string CategoryId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public int JobCount { get; set; }
    }
}
