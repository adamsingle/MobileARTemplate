using System.ComponentModel;
using UnityEngine;
using UnityWeld.Binding;

[Binding]
public class ARSessionStarterView : MonoBehaviour , INotifyPropertyChanged
{
    public event PropertyChangedEventHandler PropertyChanged;

    private void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    private string _sessionState;

    [Binding]
    public string SessionState
    {
        get => _sessionState;
        set
        {
            if (value == _sessionState)
            {
                return;
            }

            _sessionState = value;
            OnPropertyChanged(nameof(SessionState));
        }
    }
}
