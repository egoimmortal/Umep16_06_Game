---
title: Unity 學習筆記
tags: Unity
---
*HackMD的話使用[TOC]*

# Project
- Custom Render Texture
> 可以放到 Camera 的 Target Texture，然後放到 UI/Raw Image 可以做出人物大頭貼的效果
## Edit/Project Settings/Quality
    可以選擇Shadow的品質等

# 圖形化場景
## Scene View
    做操作都在這邊操作
### Shading Mode
    物件身上的顯示
- Shaded
> 顯示Shade
- WireFrame
> 網格，可以把構成幾何圖形的三角形顯示出來
- Shaded WireFrame
> 兩個同時顯示，如果人物走過去會掉落之類的可能就是有個洞
### Miscellaneous
- Shadow Cascades
> 針對影子的最佳化技術
- Render Paths		
- Alpha Channel
### 場景顯示
- Skybox
> 天空盒子，包圍整個遊戲的外殼
- Fog
> 霧
- Flares
> 鏡頭轉向太陽時顯示的光斑
- Animated Matrrials
> 動態材質
- Image Effects
> 影像後製(影像放到記憶體後做的處理)，景深，曝光
- Particle Systems
> 粒子特效
### Gizmos
- 跟顯示參考有關，例如攝影機的圖案顯示或是攝影機的參考線等等
### 方向儀
- Iso       正交投影，物件大小會一樣大，看不出深度，通常2D用
- Persp     透視，物件會因為遠近有顯示上的差距，會有深度，通常3D用

# Game View
    編輯器的遊戲視窗
## Stats
    簡易瀏覽效能跟場景資訊的東西
- Audio     聲音相關
- Graphics  繪圖相關
- FPS(ms)
- Batches
- Tris
> 多少三角形的面
- Verts
> 頂點數
- Screen
> 解析度

# Inspector
    物件的Component總覽，上方區塊有一些基本屬性，像是Tag
## Static
    要歸類在哪的Static
## Tag
    最多使用0~31個Tag
## Layer
    要在哪些Layer顯示

# GameObject
    所有的東西都是在空的物件(GameObject)上綁上Component
## Quad
> 單純的平面，由兩片三角形組合成的四邊形
## Ragdoll
> 物理控制
## Terrain
> 地形
## Sprite
    類似Quad，不過兩面都有，永遠面對攝影機，通常用來做2D元件
### Sprite Mode
- Single
> 會變成一整張圖
- Multiple
> 會有不同的小張圖
- Polygon
> 會自己切一塊顯示圖，比較少用到
## Particle System
    例子特效
## Trail
    拖曳的特效
## Model
## Character
## Light
    type選擇哪一種模式的Light
### Directional Light
    Realtime 使用
    只有方向性
1. Color
> 光源做相乘或相加
2. Mode
> Realtime時使用<br />
> Mixed，Realtime & Baked都會使用<br />
> Baked時使用
3. Intensity
> 燈的強弱
4. Indirect Multiplier
> 有間接光源的話會在Baked時乘上
5. Shadow Type
> 產生影子的方式<br />
> No Shadows<br />
> Soft Shadows，有柔邊，耗效能<br />
> Hard Shadows，邊緣較粗糙
> > Realtime Shadows
> > - Strength，影子的強弱
> > - Resolution，選擇影子的品質
> > - Bias，誤差調整
> > - Normal Bias，誤差調整
> > - Near Plane
6. Cookie
> 貼圖
7. Cookie Size
> 貼圖的Size
8. Draw Halo
9. Flare
10. Render Mode
11. Culling Mask
### Point Light
> Realtime 使用
### Spotlight
> Realtime 使用
### Area Light
> Baked Light map 使用
### Reflection Probe
> Realtime & Light map 使用
### Light Probe Group
> Baked Light map 使用
## FX
## Audio
## Model
### Model
    通常不用特別做變動
- Scale Factor
> 直接改變這個材料的屬性，會改變整體，建議不要更動
- Import BlendShapes 混和形狀
> 要不要將不同資訊帶到Unity編輯器中
- Weld Vertices
> 是否要合併頂點，做最佳化的運算
- Normals 法向量，做Lighting最重要的東西
> Import 由資料進來
- Tangents
### Rig
    跟動畫有關，通常不動，有問題再做調整
- Animation Type
> 建議不使用Legacy，Generic和Humanoid動作可能會不互通，有Humanoid就用Humanoid。
### Animation
    跟動畫有關，通常不動，有問題再做調整
- Motion
1. Root Motion Node
> 影響動畫的節點
## Transform
    Component	矩陣資料(物件預設Component) 調整都會修改矩陣，會乘上世界座標，就是現在的位置
- position
> 平移，顯示的是世界座標的位置，頂點*矩陣就是世界座標的位置
- Rotation
> 旋轉，針對Local做旋轉
- Scale
> 縮放，針對Local做縮放
## Capsule(Mesh Filter)
    儲存頂點資訊的Component的容器
- Mesh
> 頂點型態，物件模型的形狀
## Mesh Renderer
    渲染模型的地方
- Lighting
1. Light Probes
2. Reflection Probes
3. Anchor Override
4. Cast Sahdows
> 是否要產生影子和影子的計算
5. Receive Shadows
> 是否要接收影子
6. Motion Vertor
> 後製使用
7. Lightmap Static
> 是否要在Lightmap使用
- Materials
1. Size
2. Element 0
- Dynamic Occluded
> 是否會遮蔽其他人
- Default Material
> 材質球，美術匯進來時可能會有不同材質<br />
> 會影響到Draw Call
## Shinned Mesh Renderer
    皮膚變形，物件上的各個頂點會影響到其他的物件頂點，會有事前的運算
    很消耗效能的東西，多數都在CPU做運算
- Quality
> Auto(自動)，或是可以調整頂點會被骨頭影響的數量
- Update When Offscreen
> 是否要一直更新，看不到的時候就不用
- Skinned Motion Vectors
> 提供動量，後製做使用，可以做動態模糊的效果
- Mesh
- Root Bone
> 影響到動畫位移相關
- Bounds
> 通常不用調
- Lighting
- Materials
- Dynamic Occluded
> 是否會遮蔽其他人
- Default Material
> 材質球，美術匯進來時可能會有不同材質<br />
> 會影響到Draw Call
## Material
    材質，在有沒有Lighting的情況下會如何顯示
    Standard Shader是預設的Shader，不能修改
- Rendering Mode
> Opaque，不透明材質<br />
> Cutout，由Alpha Cutoff的值決定是否顯示<br />
> Fade，半透明，會混到後面的顏色<br />
> Transparent，半透明，跟Fade會有點差別，表面會有較強烈的反射光，通常用在玻璃杯
- Main Maps
1. Albedo*反照率貼圖*
> 本身是一個顏色與紋理的貼圖，顯示純色或是有貼圖的時候將貼圖跟選擇的顏色呈在一起後顯示<br />
> Alpha Cutoff，在Cutout中切換是否顯示的值，在Rendering Mode選擇Cutout才會顯示
2. Metallic *0~1*
> 金屬感，值越大越像金屬<br />
> Smoothness*0~1*，值越大反光越強
3. Normal Map
> 法向量貼圖，可以製造凹凸感，會被光影響
4. Height Map
> 有高低差的貼圖
5. Occclusion
> 幾何彼此很接近的時候造成自己的陰影
6. Detail Mask
> 會影響到Secondary Maps/Detail Albedo
7. Emission
> 自發光，加成一個顏色上去，可以高於1
8. Tiling
> X, Y，在Shader中會傳進去一個Tiling設定的XY矩陣讓UV去乘<br />
> 在做血條的時候，將血條的Scale縮小的同時將Tiling的X也變小，可以讓圖做等比例縮小
9. Offest
> 矩陣中帶有平移，適合用來做UV的動畫
- Secondary Maps
1. Detail Albedo
> 第二層貼圖，呈現細節時使用
2. Normal Map
3. Tiling
4. Offest
5. UV Set
- Forward Rendering Options
1. Specular Highlights
> 是否要在攝影機中顯示高光反射
2. Refilections
> 是否要在攝影機中顯示反射
- Advanced Options
1. Enable GPU Instancing
> 勾選的話可以在硬體渲染的時候使用硬體複製出來，可以優化效能，減少Draw Call
2. Double Sided Global
## Animator
    Component	動畫管理，管理animation等
- Layers
1. set/Weight *0~1*
> Layer的權重，會將所有Layer依照權重跟Blending方式去進行顯示
2. set/Mask
> Avatar，
3. set/Blending
> Override(覆寫)
> Additive(附加)
- Parameters
1. Float
> Float型別數字
2. Int
> Int型別數字
3. Bool
> Boolean參數
4. Trigger
> 使用一次後會被Reset
- Create State/empty
> 創建的animation state檔案會在建立在當前的Folder下
- Create State/From New Blend Tree
1. Blend Tree
> 2D Freeform Cartesian 不同狀態會有不同的動作效果，紅點會自動混接幾個藍點的動作
- Create Sub-State Machine
> 建立新的Layer，裡面可以再設定動作
- animation state
1. Has Exit Time	動畫銜接的參數
## Animation
### Curves
    調整動畫的表演方式
- Left Tangent/Free(光滑Smooth)
- Left Tangent/Linear(線性)
> 沒有內差的話有明顯停頓會比較醜

## Skybox *天空盒子，包圍整個場景的外殼*
## Camera
---
- GameObject/Move To View
> 移動選取的物件到現在的視角正中心
- GameObject/Align With View
> 調整好想要的視角後，點選攝影機，上面的 GameObject/Align With View 就可以將選取的物件移動到目前的視角
- GameObject/Align View to Selected
> 移動現在的視角到選取的物件的視角
- Clear Flags
- Skybox
> 顏色清除成Skybox的顏色
- Solid Color
> 把背景的顏色清除成Background的顏色

# UI
    重要觀念:
        九宮格:
            將圖片切成九塊，上下會隨著 Width 調整而變，左右會隨著 Height 調整而變，中間會任意放大，四個角落大小不變
	
## Rect Transform
- Anchors
> 錨點
- Pivot
> 中樞(物件原點，旋轉的時候會以這個為基準)，選取多個物件的話會取得最後一個物件的原點
- Center
> 中心位置，如果是選取多個物件則是多個物件的中心位置
## Canvas
- Reference Pixels Per Unit
> 圖形的密度，通常用預設100，盡量不會去動他
## Image
- Texture
> 使用Srpite
- preserve Aspect
> 圖片依照長寬比例縮放
- Set Native Size
> 會受到 Image/Pixels Per Unit 和 Canvas/Reference Pixels Per Unit 的比例影響
- Pixels Per Unit
> 一個單位裡面可以有多少pixel
- Image Type
1. Simple
> 會把一個四邊形切成兩個三角形
2. Sliced
> 會把一個四邊形切成九宮格，用九宮格調整大小的話可以避免變形
3. Tiled
> 調整大小時會自動增加裡面的內容去符合大小
4. Filled(最常用，可以做計量表)
> Fill Amount<br />
> 點移動的時候，UV也會跟著動，這樣圖才不會變形，通常用來做血條，如果是調整Height來達到目的的話
> 圖形會被改變，所以通常都調整Fill Amount
## Raw Image(小地圖)
- Texture
> 使用一般的圖
## Texture Type
	一般是Default	
## Panel
	也是image的一種，預設Rect Transform設定不同，適合當Group使用
## Mask & RectMask 2D
    顯示被Mask覆蓋的地方
    Mask需要加上一個Image來當顯示範圍，Mask 2D不能使用Image
	也可以使用Shader來呈現，用Mask比較方便
## Button
- Image Component
- Button Component
> 可互動<br />
> 有狀態控制及轉換過程<br />
> 預設Callback(Click發生)
- Child
> Text Object
- Interactable	是否能被互動
- Transition		變化的過程
- Color Tint	顏色變化
> Target Graphic	要變化的目標，通常一般指的是自己
- Sprite Swap	圖片變化
> Target Graphic	要變化的目標，通常一般指的是自己
- Animation	動畫變化
> Navigation

# Shader
    顏色是0~1，不是0~255
- Alpha Test
> 檢查Alpha值，決定是否畫出這個pixel
# 額外重點:
## 位移
    程式位移跟美術位移要統整使用一種來使用，不然程式就要寫兩套
- 程式位移
> 靠程式自己運算
- 美術位移
> 畫面較漂亮，不太會有滑步問題，可是位移需要額外換算，像是motion之類的設定
## 物件會被方塊包起來，決定要不要顯示是藉由那個方塊的八個頂點來決定，而不是用物件的頂點來決定
## Draw Call 的數量會嚴重影響到效能，尤其是手機平台
## 一個頂點會有 座標(x, y, z) 法向量(normal)(x, y, z) 貼圖的依據(u, v)
## UV
    圖片位置的顏色資訊，用來查圖用的，通常是0~1
## 顯示
    物件會被方塊包起來，然後由方塊決定會不會被顯示
    Root Bone和Bounds會影響到
## Shadiw Map
    通常用正投影的攝影機去照，光源是平行光
    把範圍內物件每一個pixel到攝影機之間的距離(深度)記錄下來，就是Shadow的desk map
    會將攝影機看到的Render到一張圖上，用深度比對的方式來決定看到的點有沒有被遮到