using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;


namespace ToDoList;

public partial class MainPage : ContentPage
{
    public MainPage()
	{
		InitializeComponent();
    }

    private void ClickAddButton(object sender, EventArgs e)
    {
        Input.Text = string.Empty;
    }
}

public class BaseViewModel : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}


public class MyColor
{
    public string Name { get; set; }
    public Color Value { get; set; }
    public override string ToString()
    {
        return Name;
    }
}

public class Item : BaseViewModel
{
    private string _text;
    public string Text
    {
        get => _text;
        set
        {
            if (Equals(value, _text)) return;
            _text = value;
            OnPropertyChanged(nameof(Text));
        }
    }

    private MyColor _color;
    public MyColor Color
    {
        get => _color;
        set
        {
            if (Equals(value, _color)) return;
            _color = value;
            OnPropertyChanged(nameof(Color));
        }
    }

    private bool _checked;

    public bool Checked
    {
        get => _checked;
        set
        {
            if (_checked == value) return;
            _checked = value;
            OnPropertyChanged(nameof(Checked));
        }
    }
}

class MainViewModel : BaseViewModel
{
    public ObservableCollection<Item> Items { get; } = new ObservableCollection<Item>();

    public MainViewModel()
    {

        SelectionColor = CollectionColors.FirstOrDefault(c => c.Name == "Без категории");

        AddCommand = new Command<string>(text =>
        {
            Items.Add(new Item{ Text = text, Color = _selectionColor});
        }, text => string.IsNullOrWhiteSpace(text) == false);

        DeleteCommand = new Command<Item>(x => Items.Remove(x), x => x != null);

    }
    public ICommand AddCommand { get; }
    public ICommand DeleteCommand { get; }



    private MyColor _selectionColor;
    public MyColor SelectionColor
    {
        get => _selectionColor;
        set
        {
            if (Equals(value, _selectionColor)) return;
            _selectionColor = value;
            OnPropertyChanged(nameof(SelectionColor));
        }
    }

    public Collection<MyColor> CollectionColors { get; } = new Collection<MyColor>()
    {
        new MyColor { Name = "Без категории", Value = Colors.White },
        new MyColor { Name = "Красный", Value = Colors.Red },
        new MyColor { Name = "Желтый", Value = Colors.Gold },
        new MyColor { Name = "Зеленый", Value = Colors.Green },
        new MyColor { Name = "Синий", Value = Colors.Blue },
        new MyColor { Name = "Фиолетовый", Value = Colors.DarkViolet },

    };
}

