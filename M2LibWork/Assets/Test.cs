using M2Lib;
using System.Threading.Tasks;
using UnityEngine;

public class Test : MonoBehaviour
{
    private void Start()
    {
        _ = SaveLoadAsync(null);
        _ = SaveLoadAsync("sample");
    }

    private void Update()
    {
    }

    private async ValueTask SaveLoadAsync(string name)
    {
        var testUserData = await SaveData.LoadAsync<TestUserData>(name);
        if (testUserData == null)
        {
            testUserData = new TestUserData { Money = 0 };
        }
        testUserData.Money++;
        await SaveData.SaveAsync(testUserData, name);
    }
}
