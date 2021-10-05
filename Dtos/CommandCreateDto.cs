namespace Commander.Dtos
{
    public class CommandCreateDto
    {
        [System.ComponentModel.DataAnnotations.Required]
        [System.ComponentModel.DataAnnotations.MaxLength(250)]
        public string HowTo { get; set; }

        [System.ComponentModel.DataAnnotations.Required]
        [System.ComponentModel.DataAnnotations.MaxLength(250)]
        public string Line { get; set; }
        
        [System.ComponentModel.DataAnnotations.Required]
        [System.ComponentModel.DataAnnotations.MaxLength(250)]
        public string Platform { get; set; }

    }//public class CommandCreateDto
}//namespace Commander.Dtos