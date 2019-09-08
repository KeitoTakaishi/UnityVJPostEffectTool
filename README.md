# UnityVJPostEffectTool


![GridFlashEffect](https://github.com/KeitoTakaishi/UnityVJPostEffectTool/blob/master/bandicam%202019-08-29%2000-55-38-266.jpg)

![ZoomEffect](https://github.com/KeitoTakaishi/UnityVJPostEffectTool/blob/master/bandicam%202019-08-29%2000-56-04-202.jpg)

![GlitchEffect](https://github.com/KeitoTakaishi/UnityVJPostEffectTool/blob/master/bandicam%202019-08-29%2000-55-55-178.jpg)



## Description
This is PostEffect package(HLSL).
### Containts
- InvertColor
- Zoom
- RGBShift
- Glitch
- GridFlash
- VerticalSymmetry
- HorizontalSymmetry
- Mosaic
- Tile
- FeedBack
- SobelEdge
## Usage
0. Please see ExampleScene!

1. PostEffectApply.cs attach camera

2. - Select SwitchMode(in PostEffectApply).
  - HumanMode : A player can select effect myselef.
  - AutoMode : Effects are randomly selected every PostEffectApply.EffectSpan(in inspector).
  - Momentary-HumanMode is a player can select effect myselef, and effect returns default after the lapse of the effect time. You can material some EffectTime and other parameters in inspector.
