using System;
using Xunit;
using Moq;
using TodoApi.Models;
using TodoApi.Controllers;
using System.Collections.Generic;

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
                    Title = "Trial",
                    CheckList = new List<CheckListItem>{
                        new CheckListItem{
                            Id = 2,
                            Text = "Try Xunit",
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

        [Fact]
        public void RetrieveAll_Positive()
        {
            var datarepo = new Mock<IDataRepo>();
            List<Note> notes = GetMockDatabase();
            datarepo.Setup(d => d.RetrieveAll()).Returns(notes);
            TodoController todoController = new TodoController(datarepo.Object);
            var result = todoController.Get();
            Assert.NotNull(result);
            Assert.Equal(2 , notes.Count);
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
        }
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
    }
}