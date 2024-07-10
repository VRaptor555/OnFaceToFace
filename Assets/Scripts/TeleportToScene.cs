using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TeleportToScene : MonoBehaviour
{
    [SerializeField] private string toScene;
    [SerializeField] private float distanceActivate;
    [SerializeField] private BaseItemData itemToGoto;

    private Rigidbody2D rb;
    private RaycastHit2D hit;
    private GameObject objectToTeleport;
    private InventoryController _inventoryController;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        hit = Physics2D.Raycast(rb.position, Vector2.up, distanceActivate, LayerMask.GetMask("Player"));
        if (hit)
        {
            objectToTeleport = hit.transform.gameObject;
        }
        else
        {
            hit = Physics2D.Raycast(rb.position, Vector2.down, distanceActivate, LayerMask.GetMask("Player"));
            if (hit)
            {
                objectToTeleport = hit.transform.gameObject;
            }
            else
            {
                objectToTeleport = null;
            }
        }

    }

    public void ActivateTeleport()
    {
        if (objectToTeleport)
        {
            if (itemToGoto is null) // Если нет предмета для прохода
            {
                StartCoroutine(LoadYourAsyncScene());
            }
            else
            {
                // Проверяем, клали предмет для прохода уже?
                if (PlayerGameObjects.findObject(gameObject.name + "_togo"))
                {
                    StartCoroutine(LoadYourAsyncScene());
                }
                else
                {
                    // Предмет еще не клали, ищем в инвентаре
                    _inventoryController = GameObject.FindGameObjectWithTag("Inventory").GetComponent<InventoryController>();
                    bool __found = false;
                    foreach (var item in _inventoryController.items)
                    {
                        var data = item.Data;
                        if (data)
                        {
                            if (data.name == itemToGoto.name) // Нашли предмет в инвентаре
                            {
                                __found = true;
                                PlayerGameObjects.addToList(gameObject.name + "_togo");
                                _inventoryController.DelItem(item.Data, 1);
                                StartCoroutine(LoadYourAsyncScene());
                                break;
                            }
                        }
                    }

                    if (!__found)
                    {
                        // Нет предмета для прохода
                        var messageShow = GameObject.FindGameObjectWithTag("MS_Canvas").GetComponent<MessageShow>();
                        messageShow.showMessage(itemToGoto.Icon, "Дя прохода необходим предмет: \"" + itemToGoto.name + "\"");
                    }
                }
            }
        }
    }
    private IEnumerator LoadYourAsyncScene()
    {
        Debug.Log("Goto new scene");
        // Set the current Scene to be able to unload it later
        Scene currentScene = SceneManager.GetActiveScene();

        // The Application loads the Scene in the background at the same time as the current Scene.
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(toScene, LoadSceneMode.Additive);
        Debug.Log("Async Load: "+ asyncLoad.isDone);
        // Wait until the last operation fully loads to return anything
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
        Debug.Log("Async Load: "+ asyncLoad.isDone);

        float p_x = Variables.Object(gameObject).Get<float>("Player_x");
        float p_y = Variables.Object(gameObject).Get<float>("Player_y");
        var vector = new Vector3(p_x, p_y, 0.5f);
        objectToTeleport.transform.position = vector;
        Camera.main.transform.position = vector;
        if (toScene != "Game_Level1")
        {
            SceneManager.MoveGameObjectToScene(objectToTeleport, SceneManager.GetSceneByName(toScene));
            SceneManager.MoveGameObjectToScene(Camera.main.gameObject, SceneManager.GetSceneByName(toScene));
        }
        else
        {
            SceneManager.MoveGameObjectToScene(objectToTeleport, SceneManager.GetSceneByName(toScene));
            SceneManager.MoveGameObjectToScene(Camera.main.gameObject, SceneManager.GetSceneByName(toScene));
        }
        // Unload the previous Scene
        SceneManager.UnloadSceneAsync(currentScene);
    }
}
