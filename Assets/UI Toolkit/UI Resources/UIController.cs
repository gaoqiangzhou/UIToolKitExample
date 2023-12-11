using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using DG.Tweening;
public class UIController : MonoBehaviour
{
    private VisualElement _bottomContainer;
    private Button _openButton;
    private Button _closeButton;
    private VisualElement _bottomSheet;
    private VisualElement _scrim;
    private VisualElement _boy;
    private VisualElement _girl;
    private Label _message;
    // Start is called before the first frame update
    void Start()
    {
        //access to each elements
        var root = GetComponent<UIDocument>().rootVisualElement;
        _bottomContainer = root.Q<VisualElement>("Container_Bottom");
        _openButton = root.Q<Button>("Button_Open");
        _closeButton = root.Q<Button>("Button_Close");
        _bottomSheet = root.Q<VisualElement>("Bottom_Sheet");
        _scrim = root.Q<VisualElement>("Scrim");
        _bottomContainer.style.display = DisplayStyle.None;
        _boy = root.Q<VisualElement>("Boy");
        _girl = root.Q<VisualElement>("Girl");
        _message = root.Q<Label>("Message");

        //callbacks
        _openButton.RegisterCallback<ClickEvent>(OnOpenButtonClick);
        _closeButton.RegisterCallback<ClickEvent>(OnCloseButtonClick);
        _bottomContainer.RegisterCallback<TransitionEndEvent>(OnBottomSheetDown);
        //operations
        Invoke("AnimateBoy", .1f);//call method after first frame, becasue when a scene starts, an element has no previous state
    }
    private void AnimateBoy()
    {
        _boy.RemoveFromClassList("image-boy-float");
    }
    private void AnimateGirl()
    {
        _girl.ToggleInClassList("girl-down");
        _girl.RegisterCallback<TransitionEndEvent>
        (
            evt => _girl.ToggleInClassList("girl-down")
        );
    }
    private void OnOpenButtonClick(ClickEvent e)
    {
        _bottomContainer.style.display = DisplayStyle.Flex;
        _bottomSheet.AddToClassList("buttomSheet_up");
        _scrim.AddToClassList("scrim-fadeIn");
        AnimateGirl();
        _message.text = string.Empty;
        string m = "In no impression assistance contrasted. Manners she wishing justice hastily new anxious. At discovery discourse departure objection we.";
        DOTween.To(() => _message.text, x => _message.text = x, m, 3f).SetEase(Ease.Linear);
    }
    private void OnCloseButtonClick(ClickEvent e)
    {
        _scrim.RemoveFromClassList("scrim-fadeIn");
        _bottomSheet.RemoveFromClassList("buttomSheet_up");
    }

    private void OnBottomSheetDown(TransitionEndEvent evt)
    {
        if(!_bottomSheet.ClassListContains("buttomSheet_up"))
        {
            _bottomContainer.style.display = DisplayStyle.None;
        }
    }
    // Update is called once per frame
    void Update()
    {
        //Debug.Log(_boy.ClassListContains("image-boy-float"));
    }
}
