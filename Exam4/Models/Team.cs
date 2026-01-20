using System.ComponentModel.DataAnnotations.Schema;

namespace Exam4.Models
{
    public class Team:BaseEntity
    {
     
        public string Surname { get; set; }
       
       
        public string Image { get; set; }
        public  int  PositionId { get; set; }
        public Position Position { get; set; }

    }
}
