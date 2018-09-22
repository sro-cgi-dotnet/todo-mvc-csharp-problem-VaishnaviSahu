using System.Collections.Generic;

namespace TodoApi.Models{
    public interface IDataRepo{
        Note RetrieveById(int Id);
        List<Note> RetrieveNote(string text, string type);
        List<Note> RetrieveAll();
        bool CreateNote(Note note);
        bool ModifyNote(int id, Note note);
        bool DeleteNote(int id);
    }
}