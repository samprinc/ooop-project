//Models represent the structure of data.
namespace SimpleTodoAPI.Models;

public class TodoItem
{
    //get reads the value while set set assigns a value to the property.
    public int Id { get; set; }
    public string Title { get; set; }=string.Empty;
    //empty ensures that there are no null values
    public string Description { get; set; }=string.Empty;
    public bool IsCompleted { get; set; }
    public string Email{get;set;}=string.Empty;
    
}