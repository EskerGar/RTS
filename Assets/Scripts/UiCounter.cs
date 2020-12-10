using UnityEngine;
using UnityEngine.UI;

public class UiCounter : MonoBehaviour
{
    [SerializeField] private Text zombiesCount;
    [SerializeField] private Text humansCount;

    public void RefreshZombies(int amount) => Refresh(zombiesCount, amount);

    public void RefreshHumans(int amount) => Refresh(humansCount, amount);

    private void Refresh(Text textField, int amount)
    {
        textField.text = amount.ToString();
    }
}