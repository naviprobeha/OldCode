   M S B A M L   `   `   `  ����F  BStarPRNTSDK, Version=5.4.1.0, Culture=neutral, PublicKeyToken=null)clr-namespace:StarPRNTSDKStarPRNTSDK  5          StarPRNTSDK.App   5      R NWindowsBase, Version=3.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=3.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=3.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      8x,http://schemas.microsoft.com/winfx/2006/xaml    5      Hmc;http://schemas.openxmlformats.org/markup-compatibility/2006    5      >d2http://schemas.microsoft.com/expression/blend/2008    5      )localclr-namespace:StarPRNTSDK    5        �� ShutdownMode$  OnMainWindowClose=�5   +    �� 	Resources 5
      !   StarPRNTSDK.ResourceManager   ResourceManager&    StarPRNTSDK.SelectedModelManager  SelectedModelManager+   %StarPRNTSDK.ReceiptInformationManager  ReceiptInformationManager(   "StarPRNTSDK.DisplayFunctionManager  DisplayFunctionManager  TextAnimation �� TargetProperty g� AutoReverse g� RepeatBehavior W� From	 W� To g� Duration  BaseTextBlockStyle �� 
TargetType	 �� Property
 �� Value  ExtStatusTextBlockStyle �� BasedOn �� Triggers :� RoutedEvent ��Name  MainWindowStyle �� Source �� Path  DialogStyle 	 PageTitleStyle 
 DialogTitleStyle  DialogButtonStyle  CallFunctionButtonStyle  ClearButtonStyle  ListBoxSeparatorStyle  ListBoxStyle  ListBoxItemStyle  ProgressBarStyle  PasteTextBoxStyle �� Mode  ComboBoxStyle  TextBoxStyle  CheckBoxStyle �� ScaleX �� ScaleY  ListBoxItemButtonTemplete �� 
TargetType ��Name �� Triggers P� Property P� Value �� 
TargetName  RefreshImage �� 	UriSource  RefreshButtonTemplete �Name  ListBoxItemTemplate �� DataType!   StarPRNTSDK.BaseListBoxItem �� Orientation � ColumnDefinitions ��Name  �� Binding! �� Value" N� EnterActions# N� ExitActions$ �� BeginStoryboardName	% �� X1	& �� Y1	' �� Y2	( �� X2) �� RelativeSource* �� Mode # ListBoxItemWithCheckBoxTemplate)   #StarPRNTSDK.ListBoxItemWithCheckBox%   StarPRNTSDK.ListBoxItemCheckBox+ d� 	IsChecked " ListBoxItemWithTextBoxTemplate(   "StarPRNTSDK.ListBoxItemWithTextBox$	   StarPRNTSDK.ListBoxItemTextBox, �� UpdateSourceTrigger- �� PreferredImeState!. �� PreferredImeConversionMode # ListBoxItemWithComboBoxTemplate)
   #StarPRNTSDK.ListBoxItemWithComboBox/ �� SelectedIndex ! ListBoxItemWithDetailTemplete'   !StarPRNTSDK.ListBoxItemWithDetail  TextReceipt0   Type1   Language2   	PaperSize  TextReceiptUTF8   RasterReceipt ! RasterReceiptBothScale " RasterReceiptScale # RasterCoupon $ RasterCouponRotation90 % 	TextLabel & CheckStatusFunction3   Type ' TextFunction ( GraphicFunction ) TurnOnOffFunction * CursorFunction + ContrastFunction %, !CharacterSetInternationalFunction  - CharacterSetCodePageFunction  . UserDefinedCharacterFunction2   ,StarPRNTSDK.CallDisplayExtFunctionClickEvent / DisplayTextExtCommand4   Function   StarPRNTSDK.BaseCommand5   
Executable 0 DisplayGraphicExtCommand 1 DisplayTurnOnOffExtCommand 2 DisplayCursorExtCommand 3 DisplayContrastExtCommand !4 DisplayCharacterSetExtCommand6   AdditionType )5 %DisplayUserDefinedCharacterExtCommand��%G+  &	        &	     &	 &    &	 9     &	 L     &	 �     &	 ~    7  7  &	 $    7   7   &	 �    &		 �    &	
 w    &	     &	 V    &	 E    7  &	 �    &	 P    &	 �    &	 z    &	 	    7  &	 	
    &	 �
    &	 9    &	 9    &	     &	 K    7  &	 �    7  7  &	 �    7  7  7  &	 W    7  7  7  &	     7  7  7  &	 �     &	 �"   7  7  &	 J#   7  7  &	  �#   7  7  &	! $   7  7  &	" �$   7  7  &	# �$   7  7  &	$ Y%   7  7  &	% �%   7  7  &	& 6&    &	' [&    &	( y&    &	) �&    &	* �&    &	+ �&    &	, �&    &	- 2'    &	. `'    &	/ �'    7 ' &	0 �'    7 ( &	1 �'    7 ) &	2 �'    7 * &	3 
(    7 + &	4 )(    7 , &	5 h(    7 .   5   
   6      5   
   66     5   
   6;     5   
   6'   �� 5   
   .��5      W� $ Opacity�6    . $ Forever��5      $	 1.0X�5      $	 0.0X�6)   $ 0:0:1D�62   5      �� 5   
   " ��6+   .�5      �� 	 � ��$
 40(�6)   �� 5      	 �@��TextAlignment$
 Center=�6.   �� 5      	 � ��$
 Center=�62   5      �� 5    
   8   " ��6^    5!      :� 5"      $ TextBlock.Loaded��6   [�5#      �� 5$      $ BeginStoryBoard��6*   8�� 6A   5&      5(      �� 5*   
   " [�6(   .�5+      �� 	 � ��
 6'   �� 8   $ MainWindowHeight��� 5,      	 � ��
 6&   �� 8  $ MainWindowWidth��� 5-      	 �@�
ResizeMode$
 NoResize=�6+   5.      �� 50   
   " �6$   .�51      �� 	 �@�Title$
  ��6&   �� 52      	 �@�SizeToContent$
 WidthAndHeight=�6.   �� 53      	 �@�
ResizeMode$
 NoResize=�6+   54      �� 56   
   " ��6'   .�57      �� 	 � ��$
 30(�6)   �� 58      	 � ��$

 Bold"�6+   �� 59      	 � ��$
 Center=�64   �� 5:      	 � ��

 �����6+   5;      �� 5=   
   " ��6)   .�5>      �� 	 � ��$
 20(�6)   �� 5?      	 � ��$
 Center=�64   �� 5@      	 � ��

 �����6+   5A      �� 5C   
   " ��6*   .�5D      �� 	 � ��$	
 100��6&   5E      �� 5G   
   " ��60   .�5H      �� 	 � ��$
 30(�6)   �� 5I      	 � ��$	
 200��6&   �� 5J      	 � ��$
 20, 10, 20, 10q�6(   �� 5K      	 � ��
 �	#FFCCCCFF6+   �� 5L      	 � ��

 ��  �6,   5M      �� 5O   
   8   " ��6\   .�5P      �� 	 � ��
 �#00FFFF6+   5Q      �� 5S   
   " ��6.   .�5T      �� 	 � ��$
 20(�6)   �� 5U      	 � ��$

 Bold"�6+   �� 5V      	 � ��

 �����6+   �� 5W      	 � ��
 6,   Z� 5X      �� 5Z   
   " ��6%   .�5[      �� 	 � ��$

 Auto=�6I   �� 5\      	 � ��$
 0q�60   5]      �� 5_   
   " ��6)   .�5`      �� 	 � ��$
 30(�6)   �� 5a      	 � ��$
 0q�6(   �� 5b      	 � ��$
 Stretch=�6;   �� 5c      	 � ��$
 Stretch=�69   5d      �� 5f   
   " �6)   .�5g      �� 	 � ��$
 25��6'   �� 5h      	 � ��$	
 400��6&   �� 5i      	 �@�IsIndeterminate
 . 60   5j      �� 5l   
   " ��6*   .�5m      �� 	 � ��$
 36(�6)   �� 5n      	 �@��TextWrapping$

 Wrap=�6-   �� 5o      	 �@��AcceptsReturn
 . 6.   �� 5p      	 � ��
 6%   �� 8   $ PasteTex�$ OneWay=�5q      �� 5s   
   " ��6&   .�5t      �� 	 � ��$
 30(�6)   �� 5u      	 � ��$
 Center=�62   �� 5v      	 � ��$
 Center=�69   5w      �� 5y   
   " ��6%   .�5z      �� 	 � ��$
 30(�6)   �� 5{      	 � ��$
 Center=�62   �� 5|      	 � ��$
 Center=�69   5}      �� 5   
   " ��6&   .�5�      �� 	 �@�LayoutTransform
 5�      �� 5�      $	 2.0Q�6%   $	 2.0Q�62   5�      �� 5�      	 � ��$
 Center=�62   �� 5�      	 � ��$
 Center=�69   �� 5�      	 � ��$
 15(�6)   5�      �� 5�   
   " ��6<   .R�5�      �� $ border��6   #��z��6%   ._�5�      �� 5�       5�      P� 5�       �@J�	IsFocused . . �5�      �� $ border��6E   	 � ��

 ��ح�5�      5�      �� 5�   
   $! Resources/Refresh_Image.png?�6+   �� 5�   
   " ��68   .R�5�      �� $��50��6   $��50��6(   .��5�      � $ Panel��6   .+�5�      �� 8��  6   5�      5�       5�      P� 5�       �@J�IsMouseOver . . �5�      �� $ Panel��6E   	 � ��

 ��ح�5�      P� 5�       � �� . . �5�      �� $ Panel��6C   	 � ��
 �#3852A45�      5�      �� 5�   
   "  63   .R�5�      �� $ Vertical=�6   .+�5�      �  5�      �� 5�      $��* �6+   �� 5�      $
��Auto �6+   .+�5�      �� $��0��6   $��2��6-   $��Stretch=�6�   8��  ��6A   �� *ListBoxItemTouchedCommand+��6o   Z� ��6�   �� *BackGroudColor+��5�      �� 5�      $ 
ButtonText��6(   $��Center=�6�   $��10, 0, 0, 0q�6�   ��6<   �� *Title+��6S   �� *ForeGroundColor+��6z   Z� ��5�   "   �� 5�   &   " ��6,    5�   *   �� 5�   .     6:   �� *IsAnimation+! True6Z   " 5�   2   �� 5�   6   $ BeginStoryboard��6F   8�� 6_   # 5�   2   �� 5�   6   $$ BeginStoryboard��6E   5�   /   5�   '   5�      5�      �� 5�      $��1��6   $��>��6,   $��Center=�6e   ��68   Z� ��6N   Z� y�6�   �� *ArrowVisibility+5�      �� 5�      $��Center=�6   $% 0��5�   '   $& 0��5�   '   $' 0��6.   $��2��5�   K   ( 5�   .   �� $ ActualWidth�) �� $
* Self=���5�   '   �� *BorderBlushColor+6K   5�      5�      �� 5�   
   "  6?   .R�5�      �� $ Vertical=�6   .+�5�      �  5�      �� 5�      $��* �6+   �� 5�      $
��Auto �6+   .+�5�      �� $��0��6   $��2��6-   $��Stretch=�6�   8��  ��6A   �� *ListBoxItemTouchedCommand+��6o   Z� ��6�   �� *BackGroudColor+��5�      �� 5�      $ 
ButtonText��6(   $��Center=�6�   $��10, 0, 0, 0q�6�   ��6<   �� *Title+��6S   �� *ForeGroundColor+��6z   Z� ��5�   "   �� 5�   &   " ��6,    5�   *   �� 5�   .     6:   �� *IsAnimation+! True6Z   " 5�   2   �� 5�   6   $ BeginStoryboard��6F   8�� 6_   # 5�   2   �� 5�   6   $$ BeginStoryboard��6E   5�   /   5�   '   5�      5�        5�      $��1��60   8�� $��0, 0, 20, 0q�6�   ��6g   �� *CheckBoxContent++ 6�   �� *CheckBoxIsChecked+$ TwoWay=�{�6�   �� *ControlIsEnabled+6�   5�      �� 5�      $��Center=�6   $% 0��5�   '   $& 0��5�   '   $' 0��6.   $��2��5�   K   ( 5�   .   �� $ ActualWidth�) �� $
* Self=���5�   '   �� *BorderBlushColor+6K   5�      5�      �� 5�   
   "  6>   .R�5�      �� $ Vertical=�6   .+�5�      �  5�      �� 5�      $��* �6+   �� 5�      $
��Auto �6+   .+�5�      �� $��0��6   $��2��6-   $��Stretch=�6�   8��  ��6A   �� *ListBoxItemTouchedCommand+��6o   Z� ��6�   �� *BackGroudColor+��5�      �� 5�      $ 
ButtonText��6(   $��Center=�6�   $��10, 0, 0, 0q�6�   ��6<   �� *Title+��6S   �� *ForeGroundColor+��6z   Z� ��5   "   �� 5  &   " ��6,    5  *   �� 5  .     6:   �� *IsAnimation+! True6Z   " 5  2   �� 5  6   $ BeginStoryboard��6F   8�� 6_   # 5  2   �� 5  6   $$ BeginStoryboard��6E   5
  /   5  '   5     5     	  5     $��1��6/   8�� $	��400��6s  $��0, 0, 20, 0q�6  ��6e   �� *TextBoxText+$ TwoWay=�$, PropertyChanged=�- 6�   �� *TextBoxPreferredImeState+. 6�   �� *#!TextBoxPreferredImeConversionMode+{�6L  �� *ControlIsEnabled+6  5     �� 5     $��Center=�6   $% 0��5  '   $& 0��5  '   $' 0��6.   $��2��5  K   ( 5  .   �� $ ActualWidth�) �� $
* Self=���5  '   �� *BorderBlushColor+6K   5     5     �� 5  
   " 
 6?   .R�5     �� $ Vertical=�6   .+�5     �  5     �� 5      $��* �6+   �� 5!     $
��Auto �6+   .+�5$     �� $��0��6   $��2��6-   $��Stretch=�6�   8��  ��6A   �� *ListBoxItemTouchedCommand+��6o   Z� ��6�   �� *BackGroudColor+��5%     �� 5&     $ 
ButtonText��6(   $��Center=�6�   $��10, 0, 0, 0q�6�   ��6<   �� *Title+��6S   �� *ForeGroundColor+��6z   Z� ��5'  "   �� 5(  &   " ��6,    5)  *   �� 5*  .     6:   �� *IsAnimation+! True6Z   " 5+  2   �� 5,  6   $ BeginStoryboard��6F   8�� 6_   # 5.  2   �� 5/  6   $$ BeginStoryboard��6E   51  /   53  '   55     57     �� 59     $��1��6   8�� $��0, 0, 20, 0q�6�   ��6V   �� *ComboBoxContents+/ 6   �� *SelectedIndex+$ TwoWay=�{�6�   �� *ControlIsEnabled+6�   5:     �� 5<     $��Center=�6   $% 0��5=  '   $& 0��5>  '   $' 0��6.   $��2��5?  K   ( 5=  .   �� $ ActualWidth�) �� $
* Self=���5?  '   �� *BorderBlushColor+6K   5@     5A     �� 5C  
   "  6=   .R�5D     �� $ Vertical=�6   .+�5E     �  5F     �� 5G     $
��Auto �6+   �� 5H     $��* �6+   �� 5I     $
��Auto �6+   .+�5L     �� $��0��6   $��Center=�6F   $��10, 0, 0, 0q�6a   ��6,   �� *Title+6a   �� 5N     $��2��6   $��Center=�6n   $��0, 0, 20, 0q�6�   ��6,   �� *Detail+��6G   �� *ForeGroundColor+6�   5O     �� 5Q     $��Center=�6   $% 0��5R  '   $& 0��5S  '   $' 0��6.   
�������$��2��5T  :   ( 5R  .   �� $ ActualWidth�) �� $
* Self=�5T  :   5U     5V       5X  
   $
0 Text=�6O   1 6[   �� 8   $ Language�2 6�   �� 8  $ 	PaperSize�  5Y  
   $0 TextUTF8=�6S   1 6c   �� 8   $ Language�2 6�   �� 8  $ 	PaperSize�  5Z  
   $0 Raster=�6Q   1 6`   �� 8   $ Language�2 6�   �� 8  $ 	PaperSize�  5[  
   $0 RasterBothScale=�6Z   1 6q   �� 8   $ Language�2 6�   �� 8  $ 	PaperSize�  5\  
   $0 RasterScale=�6V   1 6i   �� 8   $ Language�2 6�   �� 8  $ 	PaperSize�  5]  
   $0 RasterCoupon=�6P   1 6d   �� 8   $ Language�2 6�   �� 8  $ 	PaperSize�  5^  
   $0 RasterCouponRotation90=�6Z   1 6x   �� 8   $ Language�2 6�   �� 8  $ 	PaperSize�  5_  
   $0 	TextLabel=�6M   1 6^   �� 8   $ Language�2 6�   �� 8  $ 	PaperSize�  5a  
   $3 CheckStatus=�6C     5b  
   $
3 Text=�6<     5c  
   $3 Graphic=�6?     5d  
   $3 	TurnOnOff=�6A     5e  
   $3 Cursor=�6>     5f  
   $3 Contrast=�6@     5g  
   $3 CharacterSetInternational=�6Q     5h  
   $3 CharacterSetCodePage=�6L     5i  
   $3 UserDefinedCharacter=�6L     5k  
   84   5 . 6x     5l  
   84   5 . 6~     5m  
   84   5 . 6�     5n  
   84   5 . 6|     5o  
   84   5 . 6�     5p  
   84   $6 CharacterSetCodePage=�6�   5 . 6�     5q  
   84   5 . 6�   5s     5u     