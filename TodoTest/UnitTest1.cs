using System;
using Xunit;
using Moq;
using TodoApi;
using TodoApi.Models;
using TodoApi.Controllers;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace TodoApi.Tests
{
    public class TodoApiTests
    {

        private List<Note> GetMockDatabase(){
            return new List<Note>{
                new Note{
                    NoteId = 1,
                    Title = "To do list",
                    CheckList = new List<CheckListItem>{
                        new CheckListItem{
                            Id = 1,
                            Text = "Visit Tirupathi",
                            NoteId = 1
                        }
                    },
                    Labels = new List<Label>{
                        new Label{
                            Id = 1,
                            Name = "Hangout"
                        }
                    }
                },
                new Note{
                    NoteId = 2,
                    Title = "Weekend",
                    CheckList = new List<CheckListItem>{
                        new CheckListItem{
                            Id = 2,
                            Text = "Complete Assignment",
                            NoteId = 2
                        }
                    },
                    Labels = new List<Label>{
                        new Label{
                            Id = 2,
                            Name = "Trial"
                        }
                    }
                }
            };
        }

      /*  [Fact]
        public void RetrieveAll_Positive()
        {
            var datarepo = new Mock<IDataRepo>();
            List<Note> notes = GetMockDatabase();
            datarepo.Setup(d => d.RetrieveAll()).Returns(notes);
            TodoController todoController = new TodoController(datarepo.Object);
            var actionResult = todoController.Get();
           var okObjectResult = actionResult as OkObjectResult;
            Assert.NotNull(okObjectResult);
            var model = okObjectResult.Value as List<Note>;
            Assert.NotNull(model);
            Assert.Equal(notes.Count, model.Count);
        }

         [Fact]
        public void RetrieveById_negative()
        {
            var datarepo = new Mock<IDataRepo>();
            List<Note> notes = GetMockDatabase();
            int id = 3;
            datarepo.Setup(d => d.RetrieveById(id)).Returns(notes.Find(n => n.NoteId == id));
            TodoController todoController = new TodoController(datarepo.Object);
            var result = todoController.Get(id);
            Assert.Null(result.Value);
        }   */

        [Fact]
             public void RetrieveById_positive(){
             var datarepo = new Mock<IDataRepo>();
             List<Note> notes = GetMockDatabase();
             int id = 1;
             datarepo.Setup(d => d.RetrieveById(id)).Returns(notes.Find(n => n.NoteId == id));
             TodoController todoController = new TodoController(datarepo.Object);
             var result = todoController.Get(id);
             Assert.NotNull(result);
             Assert.Equal(id, result.Value.NoteId);
         }
        [Fact]
        public void DeleteNote_Positive()
        {
            var datarepo = new Mock<IDataRepo>();
            int id = 1;
            datarepo.Setup(d => d.DeleteNote(id)).Returns(true);
            TodoController todoController = new TodoController(datarepo.Object);
            var actionResult = todoController.Delete(id);
            var okObjectResult = actionResult as OkObjectResult;
            Assert.NotNull(okObjectResult);
        }
         [Fact]
        public void DeleteNote_Negative()
        {
            var datarepo = new Mock<IDataRepo>();
            int id = 5;
            datarepo.Setup(d => d.DeleteNote(id)).Returns(false);
            TodoController todoController = new TodoController(datarepo.Object);
            var actionResult = todoController.Delete(id);
            var nfObjectResult = actionResult as NotFoundObjectResult;
            Assert.NotNull(nfObjectResult);
        }
         [Fact]
        public void ModifyNote_Positive()
        {
            var datarepo = new Mock<IDataRepo>();
            Note note = new Note
            {
                NoteId = 4,
                Title = "Testing Post"
            };
            int id = (int)note.NoteId;
            datarepo.Setup(d => d.ModifyNote(id, note)).Returns(true);
            TodoController todoController = new TodoController(datarepo.Object);
            var actionResult = todoController.Put(id, note);
            var crObjectResult = actionResult as CreatedResult;
            Assert.NotNull(crObjectResult);
            var model = crObjectResult.Value as Note;
            Assert.Equal(id, model.NoteId);
        }

        [Fact]
        public void ModifyNote_Negative()
        {
            var datarepo = new Mock<IDataRepo>();
            Note note = new Note
            {
                NoteId = 4,
                Title = "Testing Post"
            };
            int id = (int)note.NoteId;
            datarepo.Setup(d => d.ModifyNote(id,note)).Returns(false);
            TodoController todoController = new TodoController(datarepo.Object);
            var actionResult = todoController.Put(id, note);
            var nfObjectResult = actionResult as NotFoundObjectResult;
            Assert.NotNull(nfObjectResult);
        }

    }
}