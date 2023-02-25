using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIGridRenderer : Graphic
{
   public Vector2Int gridSize = new Vector2Int(1, 1);
   public float thickness = 10f;

   float width;
   float height;
   float cellWidth;
   float cellHeight;

   protected override void OnPopulateMesh(VertexHelper vh)
   {
      vh.Clear();

      width = rectTransform.rect.width;
      height = rectTransform.rect.height;

      cellWidth = width / (float)gridSize.x;
      cellHeight = height / (float)gridSize.y;

      int count = 0;

      for (int y = 0; y < gridSize.y; y++)
      {
         for (int x = 0; x < gridSize.x; x++)
         {
            DrawCell(x, y, count, vh);
            count++;
         }
      }
   }

   private void DrawCell(int x, int y, int index, VertexHelper vh)
   {
      float xPos = cellWidth * x;
      float yPos = cellWidth * y;

      UIVertex vertex = UIVertex.simpleVert;

      vertex.position = new Vector3(xPos, yPos);
      vh.AddVert(vertex);

      vertex.position = new Vector3(xPos, yPos + cellHeight);
      vh.AddVert(vertex);

      vertex.position = new Vector3(xPos + cellWidth, yPos + cellHeight);
      vh.AddVert(vertex);

      vertex.position = new Vector3(xPos + cellWidth, yPos);
      vh.AddVert(vertex);

      //   vh.AddTriangle(0, 1, 2);
      //   vh.AddTriangle(2, 3, 0);

      float widthSqr = thickness * thickness;
      float distanceSqr = widthSqr / 2f;
      float distance = Mathf.Sqrt(distanceSqr);

      vertex.position = new Vector3(xPos+distance, yPos+distance);
      vh.AddVert(vertex);

      vertex.position = new Vector3(xPos + distance, yPos + (cellHeight - distance));
      vh.AddVert(vertex);

      vertex.position = new Vector3(xPos + (cellWidth - distance), yPos+ (cellHeight - distance));
      vh.AddVert(vertex);

      vertex.position = new Vector3(xPos + (cellWidth - distance), yPos + distance);
      vh.AddVert(vertex);

      int offset = index * 8;


      vh.AddTriangle(offset + 0, offset + 1, offset + 5);
      vh.AddTriangle(5, 4, 0);

      vh.AddTriangle(1, 2, 6);
      vh.AddTriangle(6, 5, 1);

      vh.AddTriangle(2, 3, 7);
      vh.AddTriangle(7, 6, 2);


      vh.AddTriangle(3, 0, 4);
      vh.AddTriangle(4, 7, 3);
   }
}
