﻿namespace ToDoListApp
{
    public class TaskItem
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public bool IsCompleted { get; set; }

        public TaskItem(int id, string description)
        {
            Id = id;
            Description = description;
            IsCompleted = false;
        }

        public override string ToString()
        {
            return $"{Id}. {(IsCompleted ? "[X]" : "[ ]")} {Description}";
        }
    }
}