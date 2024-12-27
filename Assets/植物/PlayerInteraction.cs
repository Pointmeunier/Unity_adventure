using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class PlayerInteraction : MonoBehaviour
{
    public GameObject interactionImage; // 提示框 (UI Image)
    public Vector2 offset;             // 偏移量

    private ItemBase currentItem;       // 当前碰撞的物品
    private bool canInteract = false;  // 是否可以交互
    private PlayerInputActions inputActions; // 新输入系统的动作类

    void Awake()
    {
        // 初始化输入动作
        inputActions = new PlayerInputActions();

        // 绑定交互事件
        inputActions.Gameplay.Interact.performed += OnInteract;
    }

    void OnEnable()
    {
        // 启用输入动作
        inputActions.Enable();
    }

    void OnDisable()
    {
        // 禁用输入动作
        inputActions.Disable();
    }

    void Start()
    {
        // 确保提示框初始时隐藏
        if (interactionImage != null)
        {
            interactionImage.SetActive(false);
        }
    }

    void Update()
    {
        // 持续更新提示框位置
        if (canInteract && interactionImage != null && currentItem != null)
        {
            UpdateInteractionImagePosition();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 检测是否是物品
        ItemBase item = collision.GetComponent<ItemBase>();
        if (item != null)
        {
            // 记录当前物品
            currentItem = item;
            canInteract = true;

            // 显示提示框
            if (interactionImage != null)
            {
                interactionImage.SetActive(true);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // 如果玩家离开物品范围
        if (collision.GetComponent<ItemBase>() == currentItem)
        {
            // 清除当前物品
            currentItem = null;
            canInteract = false;

            // 隐藏提示框
            if (interactionImage != null)
            {
                interactionImage.SetActive(false);
            }
        }
    }

    private void UpdateInteractionImagePosition()
    {
        if (currentItem != null)
        {
            // 获取目标物件在世界空间的右上角
            Vector3 worldPosition = currentItem.transform.position + new Vector3(0.7f, 0.7f, 0); // 偏移量
            Vector3 screenPosition = Camera.main.WorldToScreenPoint(worldPosition);

            // 更新提示框位置
            interactionImage.GetComponent<RectTransform>().position = screenPosition + (Vector3)offset;
        }
    }

    private void OnInteract(InputAction.CallbackContext context)
    {
        if (canInteract && currentItem != null)
        {
            // 应用物品效果
            currentItem.ApplyEffect(GetComponent<PlayerController>());

            // 隐藏提示框
            if (interactionImage != null)
            {
                interactionImage.SetActive(false);
            }

            // 清除当前物品
            currentItem = null;
            canInteract = false;
        }
    }

    public void OnInteractButton()
    {
        if (canInteract && currentItem != null)
        {
            // 应用物品效果
            currentItem.ApplyEffect(GetComponent<PlayerController>());

            // 隐藏提示框
            if (interactionImage != null)
            {
                interactionImage.SetActive(false);
            }

            // 清除当前物品
            currentItem = null;
            canInteract = false;
        }
    }
}
