using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
using System.Collections;

public enum InputsName { Action, Magic, Jump, Horizontal, Vertical, Scroll, Start, Select };

public class GameInput : MonoBehaviour
{

    //Controles do nosso jogo.
    private static KeyCode _Action = KeyCode.A;
    private static KeyCode _Magic = KeyCode.S;
    private static KeyCode _Jump = KeyCode.D;
    private static KeyCode _HorizontalLeft = KeyCode.LeftArrow;
    private static KeyCode _HorizontalRight = KeyCode.RightArrow;
    private static KeyCode _VerticalUp = KeyCode.UpArrow;
    private static KeyCode _VerticalDown = KeyCode.DownArrow;
    private static KeyCode _ScrollUp = KeyCode.Q;
    private static KeyCode _ScrollDown = KeyCode.E;
    private static KeyCode _Start = KeyCode.Return;
    private static KeyCode _Select = KeyCode.Escape;
    private static KeyCode[] Controles = new KeyCode[11] { _Action, _Magic, _Jump, _HorizontalLeft, _HorizontalRight, _VerticalUp, _VerticalDown, _ScrollUp, _ScrollDown, _Start, _Select };
    //Numeros que ocilam(?) quando aperta um axis
    private static float _Horizontal = 0;
    private static float _Vertical = 0;
    private static float _Scroll = 0;
    //Sensibilidade
    private static float _SensiHorizontal = 0.05f;
    private static float _SensiVertical = 0.05f;
    private static float _SensiScroll = 0.05f;
    //Diferente do input manager normal, eu estou usando os controles como base, para saber o que o jogador apertou independente da tecla.
    public static bool GetKeyDown(InputsName name)
    {
        if (Input.GetKeyDown(ConvertInputToKey(name)))
        {
            return true;
        }
        return false;
    }

    //Apartir do enum, eu devolvo a key.
    private static KeyCode ConvertInputToKey(InputsName name)
    {

        switch (name)
        {
            case InputsName.Action:
                return _Action;
            case InputsName.Magic:
                return _Magic;
            case InputsName.Jump:
                return _Jump;
            case InputsName.Start:
                return _Start;
            case InputsName.Select:
                return _Select;
        }

        return KeyCode.End;
    }

    //Se um Axis foi pressionado, ele método avisa
    public static bool GetAxisDown(InputsName name)
    {
        if (name == InputsName.Horizontal)
        {
            if (Input.GetKeyDown(_HorizontalLeft))
            {
                return true;
            }
            if (Input.GetKeyDown(_HorizontalRight))
            {
                return true;
            }
        }
        if (name == InputsName.Vertical)
        {
            if (Input.GetKeyDown(_VerticalUp))
            {
                return true;
            }
            if (Input.GetKeyDown(_VerticalDown))
            {
                return true;
            }
        }
        if (name == InputsName.Scroll)
        {
            if (Input.GetKeyDown(_ScrollUp))
            {
                return true;
            }
            if (Input.GetKeyDown(_ScrollDown))
            {
                return true;
            }
        }
        return false;
    }
    public static float GetAxisRaw(InputsName name)
    {
        if (name == InputsName.Horizontal)
        {
            if (Input.GetKey(_HorizontalLeft))
            {
                return -1;
            }
            if (Input.GetKey(_HorizontalRight))
            {
                return 1;
            }
        }
        if (name == InputsName.Vertical)
        {
            if (Input.GetKey(_VerticalUp))
            {
                return 1;
            }
            if (Input.GetKey(_VerticalDown))
            {
                return -1;
            }
        }
        if (name == InputsName.Scroll)
        {
            if (Input.GetKey(_ScrollUp))
            {
                return 1;
            }
            if (Input.GetKey(_ScrollDown))
            {
                return -1;
            }
        }
        return 0;
    }
    //Dependendo o botão que o jogador pressionar, vou retornar -1,0 ou 1
    public static float GetAxis(InputsName name)
    {
        float _temp = 0;
        if (name == InputsName.Horizontal)
        {
            _temp = _Horizontal;
        }
        if (name == InputsName.Vertical)
        {
            _temp = _Vertical;
        }

        if (name == InputsName.Scroll)
        {
            _temp = _Scroll;
        }
        return _temp;
    }

    public static bool ChangeKey(InputsName name)
    {
        KeyCode k = KeyCode.A;
        int posicao = 0;
        if (Input.anyKeyDown)
        {
            switch (name)
            {
                case InputsName.Action:
                    k = KeyPress();
                    posicao = System.Array.FindIndex<KeyCode>(Controles, x => x == k);
                    ChangeBt(posicao, _Action);
                    _Action = k;
                    break;
                case InputsName.Magic:
                    k = KeyPress();
                    posicao = System.Array.FindIndex<KeyCode>(Controles, x => x == k);
                    ChangeBt(posicao, _Magic);
                    _Magic = k;
                    break;
                case InputsName.Jump:
                    k = KeyPress();
                    posicao = System.Array.FindIndex<KeyCode>(Controles, x => x == k);
                    ChangeBt(posicao, _Jump);
                    _Jump = k;
                    break;
                case InputsName.Start:
                     k = KeyPress();
                    posicao = System.Array.FindIndex<KeyCode>(Controles, x => x == k);
                    ChangeBt(posicao, _Start);
                    _Start = k;
                    break;
                case InputsName.Select:
                    k = KeyPress();
                    posicao = System.Array.FindIndex<KeyCode>(Controles, x => x == k);
                    ChangeBt(posicao, _Select);
                    _Select = k;
                    break;
                default:
                    Debug.LogError("Esse método é só para botões e não Axis");
                    break;
            }
            return true;
        }
        return false;
    }
    public static bool ChangeAxis(InputsName name, bool negative)
    {
        KeyCode k = KeyCode.A;
        int posicao = 0;
        if (Input.anyKeyDown)
        {
            switch (name)
            {
                case InputsName.Horizontal:
                    if (negative)
                    {
                        k = KeyPress();
                        posicao = System.Array.FindIndex<KeyCode>(Controles, x => x == k);
                        ChangeBt(posicao, _HorizontalLeft);
                        _HorizontalLeft = k;
                        break;
                    }
                    else
                    {
                        k = KeyPress();
                        posicao = System.Array.FindIndex<KeyCode>(Controles, x => x == k);
                        ChangeBt(posicao, _HorizontalRight);
                        _HorizontalRight = k;
                    }
                    break;
                case InputsName.Vertical:
                    if (negative)
                    {
                        k = KeyPress();
                        posicao = System.Array.FindIndex<KeyCode>(Controles, x => x == k);
                        ChangeBt(posicao, _VerticalDown);
                        _VerticalDown = k;
                        break;
                    }
                    else
                    {
                        k = KeyPress();
                        posicao = System.Array.FindIndex<KeyCode>(Controles, x => x == k);
                        ChangeBt(posicao, _VerticalUp);
                        _VerticalUp = k;
                    }
                    break;
                case InputsName.Scroll:
                    if (negative)
                    {
                        k = KeyPress();
                        posicao = System.Array.FindIndex<KeyCode>(Controles, x => x == k);
                        ChangeBt(posicao, _ScrollDown);
                        _ScrollDown = k;
                        break;
                    }
                    else
                    {
                        k = KeyPress();
                        posicao = System.Array.FindIndex<KeyCode>(Controles, x => x == k);
                        ChangeBt(posicao, _ScrollUp);
                        _ScrollUp = k;
                    }
                    break;

                default:
                    Debug.LogError("Esse método é só para Axis e não para botões");
                    break;
            }
            return true;
        }
        return false;
    }
    private static KeyCode KeyPress()
    {
        foreach (KeyCode k in System.Enum.GetValues(typeof(KeyCode)))
        {
            if (Input.GetKeyDown(k))
            {
                return k;
            }

        }
        return KeyCode.A;//tecla padrão
    }

    private static void ChangeBt(int pos, KeyCode bt)
    {
        try
        {
            Controles[pos] = bt;
            switch (pos)
            {
                case 0:
                    _Action = bt;
                    break;
                case 1:
                    _Magic = bt;
                    break;
                case 2:
                    _Jump = bt;
                    break;
                case 3:
                    _HorizontalLeft = bt;
                    break;
                case 4:
                    _HorizontalRight = bt;
                    break;
                case 5:
                    _VerticalUp = bt;
                    break;
                case 6:
                    _VerticalDown = bt;
                    break;
                case 7:
                    _ScrollUp = bt;
                    break;
                case 8:
                    _ScrollDown = bt;
                    break;
                case 9:
                    _Start = bt;
                    break;
                case 10:
                    _Select = bt;
                    break;
            }
        }catch
        {

        }
    }
    public static string GetKeyName(InputsName name)
    {
        return ConvertInputToKey(name).ToString();
    }
    public static string GetAxisName(InputsName name, bool negative)
    {
        string _temp = string.Empty;
        switch (name)
        {
            case InputsName.Horizontal:
                if (negative)
                {
                    _temp = _HorizontalLeft.ToString();
                    break;
                }
                else
                {
                    _temp = _HorizontalRight.ToString();
                }
                break;
            case InputsName.Vertical:
                if (negative)
                {
                    _temp = _VerticalDown.ToString();
                    break;
                }
                else
                {
                    _temp = _VerticalUp.ToString();
                }
                break;
            case InputsName.Scroll:
                if (negative)
                {
                    _temp = _ScrollDown.ToString();
                    break;
                }
                else
                {
                    _temp = _ScrollUp.ToString();
                }
                break;

            default:
                Debug.LogError("Esse método é só para Axis e não para botões");
                break;
        }
        return _temp;
    }
    //Será necessário ter um GameInput na cena para que ele os axis funcionem bem.
    void Update()
    {
        #region Horizontal
        if (Input.GetKey(_HorizontalLeft))
        {
            _Horizontal -= _SensiHorizontal;
            _Horizontal = Mathf.Clamp(_Horizontal, -1f, 1f);
        }
        else if (Input.GetKey(_HorizontalRight))
        {
            _Horizontal += _SensiHorizontal;
            _Horizontal = Mathf.Clamp(_Horizontal, -1f, 1f);
        }
        else
        {
            _Horizontal = Mathf.Lerp(_Horizontal, 0, Time.deltaTime*5);
        }
        #endregion
        #region Vertical
        if (Input.GetKey(_VerticalUp))
        {
            _Vertical += _SensiVertical;
            _Vertical = Mathf.Clamp(_Vertical, -1f, 1f);
        }
        else if (Input.GetKey(_VerticalDown))
        {
            _Vertical -= _SensiVertical;
            _Vertical = Mathf.Clamp(_Vertical, -1f, 1f);
        }
        else
        {
            _Vertical = Mathf.Lerp(_Vertical, 0, Time.deltaTime*5);
        }
        #endregion
        #region Scroll
        if (Input.GetKey(_ScrollUp))
        {
            _Scroll += _SensiScroll;
            _Scroll = Mathf.Clamp(_Scroll, -1f, 1f);
        }
        else if (Input.GetKey(_ScrollDown))
        {
            _Scroll -= _SensiScroll;
            _Scroll = Mathf.Clamp(_Scroll, -1, 1);
        }
        else
        {
            _Scroll = Mathf.Lerp(_Scroll, 0, Time.deltaTime*2);
        }
        #endregion
    }
    #if UNITY_EDITOR
    [MenuItem("GameInput/Create MyInput")]
    static void CreateInput()
    {
        if (GameObject.FindObjectOfType<GameInput>() == null)
        {
            GameObject objeto = new GameObject();
            objeto.AddComponent<GameInput>();
            objeto.name = "GameInput(Nao Deletar)";
        }
        else
        {
            Debug.LogWarning("Já existe um GameInput na Cena.");
        }
    }
#endif
}
