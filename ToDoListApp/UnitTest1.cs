namespace ToDoListApp
{
    public class UnitTest1
    {
        [Fact]
        public void TaskItem_Constructor_SetsPropertiesCorrectly()
        {
            // Arrange & Act
            var task = new TaskItem(1, "Test task");

            // Assert
            Assert.Equal(1, task.Id);
            Assert.Equal("Test task", task.Description);
            Assert.False(task.IsCompleted);
        }

        [Fact]
        public void ToString_ReturnsCorrectFormat()
        {
            // Arrange
            var task = new TaskItem(1, "Test task");

            // Act
            var result = task.ToString();

            // Assert
            Assert.Equal("1. [ ] Test task", result);

            // Mark as completed and test again
            task.IsCompleted = true;
            result = task.ToString();
            Assert.Equal("1. [X] Test task", result);
        }
    }

    public class ToDoListTests
    {
        private readonly ToDoList _toDoList;

        public ToDoListTests()
        {
            _toDoList = new ToDoList();
        }

        [Fact]
        public void AddTask_ValidDescription_AddsTaskToList()
        {
            // Act
            _toDoList.AddTask("New task");

            // Assert
            var tasks = _toDoList.GetAllTasks().ToList();
            Assert.Single(tasks);
            Assert.Equal("New task", tasks[0].Description);
            Assert.False(tasks[0].IsCompleted);
        }

        [Fact]
        public void AddTask_EmptyDescription_ThrowsArgumentException()
        {
            // Act & Assert
            Assert.Throws<ArgumentException>(() => _toDoList.AddTask(""));
            Assert.Throws<ArgumentException>(() => _toDoList.AddTask("   "));
            Assert.Throws<ArgumentException>(() => _toDoList.AddTask(null));
        }

        [Fact]
        public void CompleteTask_ExistingTaskId_MarksTaskAsCompleted()
        {
            // Arrange
            _toDoList.AddTask("Task to complete");
            int taskId = _toDoList.GetAllTasks().First().Id;

            // Act
            bool result = _toDoList.CompleteTask(taskId);

            // Assert
            Assert.True(result);
            Assert.True(_toDoList.GetAllTasks().First().IsCompleted);
        }

        [Fact]
        public void CompleteTask_NonExistingTaskId_ReturnsFalse()
        {
            // Act
            bool result = _toDoList.CompleteTask(999);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void RemoveTask_ExistingTaskId_RemovesTask()
        {
            // Arrange
            _toDoList.AddTask("Task to remove");
            int taskId = _toDoList.GetAllTasks().First().Id;

            // Act
            bool result = _toDoList.RemoveTask(taskId);

            // Assert
            Assert.True(result);
            Assert.Empty(_toDoList.GetAllTasks());
        }

        [Fact]
        public void RemoveTask_NonExistingTaskId_ReturnsFalse()
        {
            // Act
            bool result = _toDoList.RemoveTask(999);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void GetAllTasks_ReturnsTasksInOrder()
        {
            // Arrange
            _toDoList.AddTask("Task 1");
            _toDoList.AddTask("Task 2");
            _toDoList.AddTask("Task 3");

            // Act
            var tasks = _toDoList.GetAllTasks().ToList();

            // Assert
            Assert.Equal(3, tasks.Count);
            Assert.Equal(1, tasks[0].Id);
            Assert.Equal(2, tasks[1].Id);
            Assert.Equal(3, tasks[2].Id);
        }

        [Fact]
        public void GetCompletedTasks_ReturnsOnlyCompletedTasks()
        {
            // Arrange
            _toDoList.AddTask("Task 1");
            _toDoList.AddTask("Task 2");
            _toDoList.AddTask("Task 3");
            _toDoList.CompleteTask(2);

            // Act
            var completedTasks = _toDoList.GetCompletedTasks().ToList();

            // Assert
            Assert.Single(completedTasks);
            Assert.Equal(2, completedTasks[0].Id);
        }

        [Fact]
        public void GetPendingTasks_ReturnsOnlyPendingTasks()
        {
            // Arrange
            _toDoList.AddTask("Task 1");
            _toDoList.AddTask("Task 2");
            _toDoList.AddTask("Task 3");
            _toDoList.CompleteTask(2);

            // Act
            var pendingTasks = _toDoList.GetPendingTasks().ToList();

            // Assert
            Assert.Equal(2, pendingTasks.Count);
            Assert.Equal(1, pendingTasks[0].Id);
            Assert.Equal(3, pendingTasks[1].Id);
        }

        [Fact]
        public void Clear_RemovesAllTasksAndResetsId()
        {
            // Arrange
            _toDoList.AddTask("Task 1");
            _toDoList.AddTask("Task 2");

            // Act
            _toDoList.Clear();

            // Assert
            Assert.Empty(_toDoList.GetAllTasks());

            // Verify ID reset
            _toDoList.AddTask("New task");
            Assert.Equal(1, _toDoList.GetAllTasks().First().Id);
        }
    }
}
