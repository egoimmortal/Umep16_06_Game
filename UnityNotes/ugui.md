# Event System
    事件的主要管理員
    管理控制器的輸入與GameObject之間的互動
    一開始建立UI的時候就會建立，不太需要做調整
# Standalone Input Module
    控制器的管理員
    一開始建立UI的時候就會建立，不太需要做調整
# Rect Transform
    UI元件都會有這個Component，繼承於Transform
- Anchor Presets
> 選擇UI的對齊方式，會隨著解析度改變位置可以用stretch來做
> - Pos X
> > 根據對齊的位置的X
> - Pos Y
> > 根據對齊的位置的Y
> - Pos Z
> > 根據對齊的位置的Z
- Anchors
> 物件的錨點，人物頭像等通常以對齊邊界為主
- Pivot
> 物件的中心點，通常以中間為主(0.5, 0.5)
- Rotation
> 物件的旋轉
- Scale
> 物件的縮放，初始縮放建議設定為1
# Canvas
    建立UI的時候都會被包含在Canvas裡，用來管理渲染物件的次序和方式
- Render Mode
> Screen Space - Overlay，不管怎麼繪製都會最後再進行繪製<br />
> Screen Space - Camera，跟著所選擇的Camera在動<br />
> World Space，只要有Event Camera，不管如何移動Camera都會停留在畫面上，純3D跟VR通常都是使用這個
> - Pixel Perfect，讓UI在某些解析度的時候會比較漂亮
> - Sort Order，Canvas之間的排序，數字越大越後畫
> - Target Display，選擇哪一個Display
> - Render Camera，選擇要Render的Camera
> - Plane Distance，選擇距離，會影響到跟3D物件的關係，會自動調整Rect Transform的Scale，通常不用調整
> - Event Camera，World Space的Camera
> - Sorting Layer，選擇Layer的繪製順序，先看這個順序才看Order in layer的順序
> - Order in Layer，Layer中繪製的順序，數字越大越後畫
- Additional Shader Channels
# Canvas Scaler
    控制子UI物件及圖的縮放，Canvas Size = Screen Size/ScaleFactor
- UI Scale Mode
> Constant Pixel Size，螢幕寬高變的時候不會縮放<br />
> Scale With Screen Size，螢幕寬高變的時候會縮放<br />
> Constant Physical Size
- Scale Factor
- Reference Pixels Per Unit
# Graphic Raycaster
    管理子UI物件的觸碰控制
- Ignore Reversed Graphics
> 是否關閉反面點擊忽略
- Blocking Objects
> 是否會被物件遮擋
- Blocking Mask
> 是否會被Mask遮擋
# Text
- ...
- Rich Text
> 是否支援HTML語法
- Horizontal Overflow
> 水平顯示類型
- Vertical Overflow
> 垂直顯示類型
- Material
> Default UI Material就夠了，除非要自己寫材質
- ...
# Image
    Image和Panel建立出來都有，通常Panel用來當Group使用，預設的Rect Transform設定不同
- Source Image
> 貼圖資源
- Color
> 顏色
- Material
> 材質
- Raycast Target
> 是否能夠被點到
- Image Type
> - Simple
> > - Preserve Aspect
> > > 是否保持長寬比
> > - Set Native Size
> > > 自動調整至適合大小的比例，會根據圖的Canvas Scaler的Reference Pixels Per Unit和Pixels Per Unit的比例決定
> - Sliced，有border才能使用，類似九宮格的Mesh，調整大小的時候四個角落不變
> > - Fill Center
> > > 中間是否要填色
> - Tiled，變動寬高的時候會重複拼貼
> > - Fill Center
> > > 中間是否要填色
> - Filled，可以用來做計量表，假如圖有漸層的話要邊調整圖邊調整UV才不會影響到解析度
> > - Fill Method
> > - Fill Origin
> > - Fill Amount
> > > 一邊移動頂點位置，一邊調整頂點的UV
> > - Clockwise
> > - Preserve Aspect
# RawImage
    一般拿來當Texture 2D在用
# Rect Mask 2D
    只能拿來當遮罩寬高
# Mask
    可以拿Image遮罩
# Input Field
    用於文字輸入
    控制字串輸入、可互動、有預設Callback
    Child: Text, Placeholder(Text)
- Interactable
> 是否可互動
- Transition
> - Target Graphic
> - Normal Color
> - Highlighted Color
> - Pressed Color
> - Disabled Color
> - Color Multiplier
> - Fade Duration
- Navigation
- Text Component
> 對應到的Text Component
- Text
> Text內容
- Character Limit
> Text字元上限，0表示無限制
- Content Type
> Text類型限制
- Placeholder
> default文字
- Caret Blink Rate
> 輸入時的閃爍線頻率
- Caret Width
> 輸入時的閃爍線寬度
- Custom Caret Color
> 輸入時的閃爍線顏色
- Selection Color
- Hide Mobile Input
- Read Only
# 額外重點:
    UI不要貼著邊框，會留一點設計，通常貼著是圖片有設計過
    3D物件是可以擋住UI的