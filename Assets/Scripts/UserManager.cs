using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.Networking;
using UnityEngine;
using System.Globalization;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class UserManager : MonoBehaviour
{
    [Header("DropDown Manager")]
    List<string> namesUserDropDown;
    public TMP_Dropdown userDropDown;

    [Header("User for the session")]
    public UserData user; //user instance in unity

    //Users variables for update
    private float contrasteToSave;
    private float lightIntensityToSave;
    private int colorOutlinesToSave;
    private float thicknessOutlinesToSave;
    private float staticLightTemperatureToSave;
    private float staticLightIntensityToSave;
    private int fontTypeToSave;
    private int fontSizeToSave;
    public int fontStyleToSave;
    private int colorMenuToSave;
    private int menuPositionToSave;
    private int thicknessBorderMenuToSave;
    private int thicknessBorderButtonToSave;
    private float colorBackgroundToSave;

    //Other variables
    private VisualizationManager visualizationManager;
    private InterfaceCustomizationManager interfaceCustomizationManager;
    private Volume postprocess;
    private LightController lightController;
    private MenuController menuController;



    void Awake()
    {
        visualizationManager = FindObjectOfType<VisualizationManager>();
        interfaceCustomizationManager = FindObjectOfType<InterfaceCustomizationManager>();
        postprocess = FindObjectOfType<Volume>();
        lightController = FindObjectOfType<LightController>();
        menuController = FindObjectOfType<MenuController>();

        userDropDown = GameObject.Find("DropdownSelectUser").GetComponent<TMP_Dropdown>();
        namesUserDropDown = new List<string>();

        StartCoroutine(LoadUsersNames());
    }

    void Start()
    {
        StartCoroutine(LoadUserFromDatabase("Start => Loading user from database"));
    }

    private void Update()
    {
        UpdateUserDataParameters();
    }

    #region LOAD USER PROFIL

    //Filled the dropdown
    public IEnumerator LoadUsersNames()
    {
        UnityWebRequest www = UnityWebRequest.Get("http://localhost/sqlconnect/IRENE/LoadUserName.php?");
        yield return www.SendWebRequest();
        string request = www.downloadHandler.text;
        request = request.Substring(0, request.Length);
        string[] requestList = request.Split('/');

        foreach (string name in requestList)
        {
            if (!string.IsNullOrEmpty(name))
            {
                namesUserDropDown.Add(name);
            }
        }

        if (userDropDown != null) //names list doesn't be empty
        {
            if (namesUserDropDown.Count > 0)
            {
                userDropDown.ClearOptions();
                userDropDown.AddOptions(namesUserDropDown); // add users names (database) at the dropdown
            }
            else
            {
                Debug.LogWarning("No users found in the database.");
            }
        }
        else
        {
            Debug.LogError("Assign a TextMeshProDropdown in the Unity inspector.");
        }
    }

    //Triggers the SELECT in database with the dropdown
    public void SelectUserProfil(int value)
    {
        string lastName = userDropDown.options[value].text;
        StartCoroutine(LoadUserFromDatabase(lastName)); 
    }

    //Retrieves from the database and updates user instance in unity
    public IEnumerator LoadUserFromDatabase(string lastName)
    {
        UnityWebRequest www = UnityWebRequest.Get("http://localhost/sqlconnect/IRENE/LoadUser.php?lastName=" + lastName);
        yield return www.SendWebRequest();
        string request = www.downloadHandler.text;

        request = request.Substring(0, request.Length);
        string[] requestList = request.Split('/');

        user.id = int.Parse(requestList[0]);
        user.lastName = requestList[1];
        user.firstName = requestList[2];
        user.age = int.Parse(requestList[3]);
        user.contraste = float.Parse(requestList[4], CultureInfo.InvariantCulture);
        user.lightIntensity = float.Parse(requestList[5], CultureInfo.InvariantCulture);
        user.colorOutlines = int.Parse(requestList[6]);
        user.thicknessOutlines = float.Parse(requestList[7], CultureInfo.InvariantCulture);
        user.staticLightTemperature = float.Parse(requestList[8], CultureInfo.InvariantCulture);
        user.staticLightIntensity = float.Parse(requestList[9], CultureInfo.InvariantCulture);
        user.fontType = int.Parse(requestList[10]);
        user.fontSize = int.Parse(requestList[11]);
        user.fontStyle = int.Parse(requestList[12]);
        user.colorMenu = int.Parse(requestList[13]);
        user.positionMenu = int.Parse(requestList[14]);
        user.thicknessBorderMenu = float.Parse(requestList[15], CultureInfo.InvariantCulture);
        user.thicknessBorderButton = float.Parse(requestList[16], CultureInfo.InvariantCulture);
        user.colorBackground = float.Parse(requestList[17], CultureInfo.InvariantCulture);

        Debug.Log("The user retrieved (with dropdown) for the session is : " + user.id + ", " + user.firstName + ", " + user.lastName);

        LoadUserParametersToUnity(user);
    }

    //Loads user parameters into unity
    public void LoadUserParametersToUnity(UserData user)
    {
        //visualization parameters
        menuController.appearanceMenu.SetActive(true);
        visualizationManager.SetVolumeContrast(user.contraste);
        visualizationManager.SetVolumeLuminosity(user.lightIntensity);
        menuController.appearanceMenu.SetActive(false);

        if (user.colorOutlines == 1)
        {
            visualizationManager.SetOutlinesColorBlack();
        }
        if (user.colorOutlines == 2)
        {
            visualizationManager.SetOutlinesColorWhite();
        }
        if (user.colorOutlines == 3)
        {
            visualizationManager.SetOutlinesColorRed();
        }
        if (user.colorOutlines == 4)
        {
            visualizationManager.SetOutlinesColorGreen();
        }
        if (user.colorOutlines == 5)
        {
            visualizationManager.SetOutlinesColorYellow();
        }
        if (user.colorOutlines == 6)
        {
            visualizationManager.SetOutlinesColorBlue();
        }
        if (user.colorOutlines == 7)
        {
            visualizationManager.SetOutlinesColorOrange();
        }
        if (user.colorOutlines == 8)
        {
            visualizationManager.SetOutlinesColorViolet();
        }

        visualizationManager.SetOutlinesBorders(user.thicknessOutlines);

        menuController.staticLightsMenu.SetActive(true);
        lightController.lightTarget.colorTemperature = user.staticLightTemperature;
        lightController.lightTarget.intensity = user.staticLightIntensity;
        menuController.staticLightsMenu.SetActive(false);

        //interface parameters
        interfaceCustomizationManager.fontsTypeDropDown.value = user.fontType;
        interfaceCustomizationManager.SetFontType();

        interfaceCustomizationManager.SetFontSize(user.fontSize);

        bool boldUserParameter;
        if (user.fontStyle == 0)
        {
            boldUserParameter = false;
        }
        else
        {
            boldUserParameter = true;
        }

        interfaceCustomizationManager.SetFontStyle(boldUserParameter);

        if (user.colorMenu == 1)
        {
            interfaceCustomizationManager.SetColorMenuWhite();
        }
        if (user.colorMenu == 2)
        {
            interfaceCustomizationManager.SetColorMenuLightGray();
        }
        if (user.colorMenu == 3)
        {
            interfaceCustomizationManager.SetColorMenuDarkGray();
        }
        if (user.colorMenu == 4)
        {
            interfaceCustomizationManager.SetColorMenuBlack();
        }

        if (user.positionMenu == 1)
        {
            interfaceCustomizationManager.MoveLeftMenu();
        }
        else
        {
            interfaceCustomizationManager.MoveRightMenu();
        }

        interfaceCustomizationManager.SetBordersMenu(user.thicknessBorderMenu);
        interfaceCustomizationManager.SetBordersButtons(user.thicknessBorderButton);

        interfaceCustomizationManager.SetSceneBackground(user.colorBackground);
    }

    #endregion

    #region CREATE USER PROFIL

    //Trigger with the create button
    public void CreateUserProfil()
    {
        string lastName = GameObject.Find("InputFieldLastName").GetComponent<TMP_InputField>().text;
        string firstName = GameObject.Find("InputFieldFirstName").GetComponent<TMP_InputField>().text;
        string age = GameObject.Find("InputFieldAge").GetComponent<TMP_InputField>().text;

        StartCoroutine(InsertUserInDatabase(lastName, firstName, age)); //TODO = int for "age"
    }

    //Triggers insert into database and updates the user instance in unity with the new user
    public IEnumerator InsertUserInDatabase(string lastName, string firstName, string age)
    {
        UnityWebRequest www = UnityWebRequest.Get("http://localhost/sqlconnect/IRENE/InsertUser.php?lastName=" + lastName + "&firstName=" + firstName + "&age=" + age);
        yield return www.SendWebRequest();
        string request = www.downloadHandler.text;

        int idUserFromDB = int.Parse(request);
        int ageInDB = int.Parse(age);

        ResetUserDataParameters(idUserFromDB, lastName, firstName, ageInDB);
    }

    //Reinitializes settings for the new user
    public void ResetUserDataParameters(int id, string lastName, string firstName, int age)
    {
        user.id = id;
        user.lastName = lastName;
        user.firstName = firstName;
        user.age = age;
        user.contraste = 0;
        user.lightIntensity = 0;
        user.colorOutlines = 1;
        user.thicknessOutlines = 0;
        user.staticLightTemperature = 10603;
        user.staticLightIntensity = 3;
        user.fontType = 0;
        user.fontSize = 1;
        user.fontStyle = 0;
        user.colorMenu = 1;
        user.positionMenu = 2;
        user.thicknessBorderMenu = 1;
        user.thicknessBorderButton = 1;
        user.colorBackground = 0;
    }

    #endregion

    #region UPDATE USER PROFIL

    //Trigger with the save button
    public void UpdateUserProfil()
    {
        Debug.Log("The updated user is  : " + user.id + ", " + user.firstName + ", " + user.lastName);

        //To protect the id of “default user + true visually impaired” profiles
        //[1 = default user, and 2 to 5 = visually impaired]
        if (user.id > 5)
        {
            StartCoroutine(UpdateUserInDatabase(user.id));

        }
        else
        {
            Debug.Log("ID is locked");
        }
    }

    public IEnumerator UpdateUserInDatabase(int userId)
    {
        LoadUserParametersFromUnity(user);

        UnityWebRequest www = UnityWebRequest.Get("http://localhost/sqlconnect/IRENE/UpdateUser.php?id=" + userId
                                                    + "&contraste=" + contrasteToSave + "&lightIntensity=" + lightIntensityToSave
                                                    + "&colorOutlines=" + colorOutlinesToSave + "&thicknessOutlines=" + thicknessOutlinesToSave
                                                    + "&staticLightTemperature=" + staticLightTemperatureToSave + "&staticLightIntensity=" + staticLightIntensityToSave
                                                    + "&fontType=" + fontTypeToSave + "&fontSize=" + fontSizeToSave + "&fontStyle=" + fontStyleToSave
                                                    + "&colorMenu=" + colorMenuToSave + "&positionMenu=" + menuPositionToSave + "&thicknessBorderMenu=" + thicknessBorderMenuToSave + "&thicknessBorderButton=" + thicknessBorderButtonToSave
                                                    + "&colorBackground=" + colorBackgroundToSave);

        yield return www.SendWebRequest();
        string request = www.downloadHandler.text;
    }

    //Retrieves user parameters from unity
    public void LoadUserParametersFromUnity(UserData user)
    {
        contrasteToSave = visualizationManager.contrasteUserVar;
        lightIntensityToSave = visualizationManager.intensityUserVar;

        colorOutlinesToSave = visualizationManager.colorOutlinesUserVar;
        thicknessOutlinesToSave = visualizationManager.thicknessOutlinesUserVar;

        staticLightTemperatureToSave = lightController.staticLightTemperature;
        staticLightIntensityToSave = lightController.staticLightIntensity;

        fontTypeToSave = interfaceCustomizationManager.fontsTypeDropDown.value;
        fontSizeToSave = interfaceCustomizationManager.fontSizeUserVar;

        if (interfaceCustomizationManager.boldCheck == false)
        {
            fontStyleToSave = 0;
        }
        else
        {
            fontStyleToSave = 1;
        }

        colorMenuToSave = interfaceCustomizationManager.colorMenuUserVar;
        menuPositionToSave = interfaceCustomizationManager.menuPositionUserVar;
        thicknessBorderMenuToSave = interfaceCustomizationManager.borderMenuUserVar;
        thicknessBorderButtonToSave = interfaceCustomizationManager.borderButtonUserVar;

        colorBackgroundToSave = interfaceCustomizationManager.backgroundColorUserVar;
    }

    //Updates settings for the user in untiy
    public void UpdateUserDataParameters()
    {
        user.contraste = visualizationManager.contrasteUserVar; ;
        user.lightIntensity = visualizationManager.intensityUserVar;
        user.colorOutlines = visualizationManager.colorOutlinesUserVar;
        user.thicknessOutlines = lightController.staticLightTemperature;
        user.staticLightTemperature = lightController.staticLightIntensity;
        user.staticLightIntensity = interfaceCustomizationManager.fontsTypeDropDown.value;
        user.fontType = interfaceCustomizationManager.fontsTypeDropDown.value;
        user.fontSize = interfaceCustomizationManager.fontSizeUserVar;
        if (interfaceCustomizationManager.boldCheck == false)
        {
            user.fontStyle = 0;
        }
        else
        {
            user.fontStyle = 1;
        }
        
        user.colorMenu = interfaceCustomizationManager.colorMenuUserVar;
        user.positionMenu = interfaceCustomizationManager.menuPositionUserVar;
        user.thicknessBorderMenu = interfaceCustomizationManager.borderMenuUserVar;
        user.thicknessBorderButton = interfaceCustomizationManager.borderButtonUserVar;
        user.colorBackground = interfaceCustomizationManager.backgroundColorUserVar;
    }

    #endregion
}

