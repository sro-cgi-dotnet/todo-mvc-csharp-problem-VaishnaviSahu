using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using TodoApi;

namespace TodoApi.Models{
    public class DatabaseRepo : IDataRepo {

        TodoContext db = null;
        public DatabaseRepo(TodoContext _db){
            this.db = _db;
        }

        public Note RetrieveById(int Id){
            return db.Notes.Include(n => n.CheckList).Include(n => n.Labels).FirstOrDefault(n => n.NoteId == Id);
        }

        public List<Note> RetrieveNote(string text, string type){
            List<Note> notesWithText = new List<Note>();
            if(type == "Label"){
                List<Label> textLabels = db.Labels.Where(l => l.Name == text).ToList();
                foreach (Label label in textLabels){
                    var retrievedNote = db.Notes.Include(n => n.CheckList).Include(n => n.Labels).FirstOrDefault(n => n.NoteId == label.NoteId);
                    notesWithText.Add(retrievedNote);
                }
            } else if(type == "Title")
            {
                notesWithText = db.Notes.Where(n => n.Title == text).Include(n => n.CheckList).Include(n => n.Labels).ToList();
            } else if(type == "Pinned")
            {
                if(text == "true"){
                    notesWithText = db.Notes.Where(n => n.IsPinned == true).Include(n => n.CheckList).Include(n => n.Labels).ToList();
                }
                else if (text == "false"){
                    notesWithText = db.Notes.Where(n => n.IsPinned == false).Include(n => n.CheckList).Include(n => n.Labels).ToList();
                }
                else{
                    return null;
                }
            } else {
                return null;
            }
            return notesWithText;
        }

        public List<Note> RetrieveAll(){
            return db.Notes.Include(n=> n.CheckList).Include(n => n.Labels).ToList();
        }

        public bool CreateNote(Note note){
            if(db.Notes.FirstOrDefault(n => n.NoteId == note.NoteId) == null){
                db.Notes.Add(note);
                //PostChecklist(note);
                db.SaveChanges();
                return true;
            }
            else{
                return false;
            }
        }
 /* 
        void PostChecklist(Note note){
            foreach(CheckListItem cl in note.CheckList){
                db.CheckLists.Add(cl);
            }
            foreach(Label l in note.Labels){
                db.Labels.Add(l);
            }
            db.SaveChanges();
        }
*/
        public bool ModifyNote(int id, Note note){
            Note retrievedNote = db.Notes.Include(n => n.CheckList).Include(n => n.Labels).FirstOrDefault(n => n.NoteId == id);
            if(retrievedNote != null){
                db.Notes.Remove(retrievedNote);
                db.Notes.Add(note);
                db.SaveChanges();
                return true;
            }
            else{
                return false;
            }
        }

        public bool DeleteNote(int id){
            Note retrievedNote = db.Notes.Include(n=>n.CheckList).Include(n=>n.Labels).FirstOrDefault(n => n.NoteId == id);
            if(retrievedNote != null){
                db.Notes.Remove(retrievedNote);
                db.SaveChanges();
                return true;
            }
            else{
                return false;
            }
        }
    }
}
