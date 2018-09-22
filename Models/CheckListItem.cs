using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TodoApi.Models
{
    public class CheckListItem {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id {get; set;}
        public bool IsChecked {get; set;}
        public string Text {get; set;}
        public int NoteId{get; set;}

    }
}