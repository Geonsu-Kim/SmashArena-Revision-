using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogManager : SingletonBase<DialogManager>
{
    private Queue<DialogData> DialogQueue;
    private Dictionary<DialogType, DialogController> DialogMap;
    private DialogController CurController;
    private DialogManager()
    {
        DialogQueue = new Queue<DialogData>();
        DialogMap = new Dictionary<DialogType, DialogController>();
    }
    public void Regist(DialogType type, DialogController controller)
    {
        DialogMap[type] = controller;
    }
    public void Push(DialogData data)
    {
        DialogQueue.Enqueue(data);
        if (CurController == null)
        {
            ShowNext();
        }
    }
    public void Pop()
    {
        if (CurController != null)
        {
            CurController.Close(delegate
            {
                CurController = null;
                if (DialogQueue.Count > 0)
                {
                    ShowNext();
                }
            });
        }
    }
    private void ShowNext()
    {
        DialogData next = DialogQueue.Peek();
        DialogController controller = DialogMap[next.Type].GetComponent<DialogController>();
        CurController = controller;
        CurController.Build(next);
        CurController.Show(delegate { });
        DialogQueue.Dequeue();
    }
    public bool IsShwoing()
    {
        return CurController != null;
    }
}
