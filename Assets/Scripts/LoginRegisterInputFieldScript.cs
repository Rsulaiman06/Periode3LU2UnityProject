using TMPro;
using UnityEngine;

public class LoginRegisterInputFieldScript : MonoBehaviour
{
    public TMP_InputField emailFromLogin;
    public TMP_InputField passwordFromLogin;
    public TMP_InputField emailFromRegister;
    public TMP_InputField passwordFromRegister;

    private UserModel user;

    void Start()
    {
        user = new UserModel();
    }

    public UserModel LoginInputFieldData()
    {
        user.email = emailFromLogin.text;
        user.password = passwordFromLogin.text;
        return user;
    }

    public UserModel RegisterInputFieldData()
    {
        user.email = emailFromRegister.text;
        user.password = passwordFromRegister.text;
        return user;
    }
}
