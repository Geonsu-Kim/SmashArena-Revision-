using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minimap : SingletonBase<Minimap>
{
    [SerializeField] Transform terrain;
    [SerializeField] RectTransform scrollViewRectTransform;
    [SerializeField] RectTransform contentRectTransform;
    [SerializeField] MinimapIcon minimapIconPrefab;

    Matrix4x4 transformationMatrix;

    Dictionary<MinimapWorldObject, MinimapIcon> MiniMapWorldObjectsLooksup = new Dictionary<MinimapWorldObject, MinimapIcon>();
    private void Start()
    {
        CalculateTransformationMatrix();
    }
    private void Update()
    {
        UpdateMiniMapIcons();
    }

    public void RegisterMiniMapWorldObject(MinimapWorldObject minimapWorldObject)
    {
        MinimapIcon minimapIcon = Instantiate(minimapIconPrefab);
        minimapIcon.transform.SetParent(contentRectTransform);
        minimapIcon.SetIcon(minimapWorldObject.Icon);
        minimapIcon.SetColor(minimapWorldObject.IconColor);
        minimapIcon.SetText(minimapWorldObject.Text);
        minimapIcon.SetTextSize(minimapWorldObject.textSize);
        MiniMapWorldObjectsLooksup[minimapWorldObject] = minimapIcon;
    }
    void UpdateMiniMapIcons()
    {
        foreach (var kvp in MiniMapWorldObjectsLooksup)
        {
            MinimapWorldObject minimapWorldObject = kvp.Key;
            MinimapIcon minimapIcon = kvp.Value;
            Vector2 mapPosition = WorldPositionToMapPosition(minimapWorldObject.transform.position);
            minimapIcon.RectTransform.anchoredPosition = mapPosition;
        }
    }
    Vector2 WorldPositionToMapPosition(Vector3 worldPos)
    {
        Vector2 pos = new Vector2(worldPos.x, worldPos.z);
        return transformationMatrix.MultiplyPoint3x4(pos);
    }
    void CalculateTransformationMatrix()
    {
        Vector2 miniMapDimensions = contentRectTransform.rect.size;
        Vector2 terrainDimensions = new Vector2(terrain.localScale.x*10, terrain.localScale.z*10);

        Vector2 scaleRatio = miniMapDimensions / terrainDimensions;
        Vector2 translation = -miniMapDimensions / 2;
        transformationMatrix = Matrix4x4.TRS(translation, Quaternion.identity, scaleRatio);
        Debug.Log(transformationMatrix);
    }
}
