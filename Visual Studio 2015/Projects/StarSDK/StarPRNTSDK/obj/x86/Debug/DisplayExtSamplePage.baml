   M S B A M L   `   `   `  ����F  BStarPRNTSDK, Version=5.4.1.0, Culture=neutral, PublicKeyToken=null)clr-namespace:StarPRNTSDKStarPRNTSDK  5      &     StarPRNTSDK.DisplayExtSamplePage   5      R NWindowsBase, Version=3.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=3.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=3.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      8x,http://schemas.microsoft.com/winfx/2006/xaml    5      Hmc;http://schemas.openxmlformats.org/markup-compatibility/2006    5      >d2http://schemas.microsoft.com/expression/blend/2008    5      )localclr-namespace:StarPRNTSDK    5      -   5          N� Title$  DisplayExtSamplePage��5	       � 	Resources 5      (   "StarPRNTSDK.DisplayFunctionManager   DisplayFunctionManagerExt��%   &	          5   
   6'   5      .��5      $   StarPRNTSDK.CustomScrollViewer  .��5   
   �  � RowDefinitions 5      �� 5      $
��Auto �6    �� 5      $
��Auto �6    �� 5      $
��Auto �6    �� 5      $
��Auto �6    �� 5      $��* �6    .+�5      �� $��0��6     PageTitleStyle#��[ 6}   ��6!   ��   ResourceManager �� Source# [  �� Path$ DisplayExtSamplePageTitle�6}   �� 5      -   5        $��0��5      $��Right=�6"     RefreshButtonTemplete#��[ 6>   $��0, 0, 20, 0q�6p   6�   �� 5      $��1��6   $��Contents��6!     ListBoxSeparatorStyle#��[ 64   �� 5      $��2��6     ListBoxItemTemplate#��[ 6#     ListBoxStyle#��[ 6n     ListBoxItemStyle#��[ 6�   ��6W   Z� .@�5       !   StarPRNTSDK.BaseListBoxItem     ForeGroundColor
 ��  �   BorderBlushColor
 �����  DisplayTextExtCommand    ListBoxItemTouchedCommand# [ 5$         Title 5!      ��  	 TextFunction# [	 $ Title�5$        5&      
 ��  �
 ����� 
 DisplayGraphicExtCommand# [
 5*       5'      ��   GraphicFunction# [ $ Title�5*        5,      
 ��  �
 �����  DisplayTurnOnOffExtCommand# [ 50       5-      ��   TurnOnOffFunction# [ $ Title�50        52      
 ��  �
 �����  DisplayCursorExtCommand# [ 56       53      ��   CursorFunction# [ $ Title�56        58      
 ��  �
 �����  DisplayContrastExtCommand# [ 5<       59      ��   ContrastFunction# [ $ Title�5<        5>      $ Character Set��5?      
 ��  �
 ����� ! DisplayCharacterSetExtCommand# [ 5B        5D      
 ��  � ) %DisplayUserDefinedCharacterExtCommand# [ 5G       5E      ��    UserDefinedCharacterFunction# [ $ Title�5G      5I      �� 5K      -   5        	 ��Name$	 StatusTextBlock��5K   %   $��3��6     ExtStatusTextBlockStyle#��[ 6>   � 5M      $��4��6   
 � ColumnDefinitions
 5N      �� 5O      $��* �6'   �� 5P      $
��Auto �6'   �� 5Q      $��* �6'   �� 5R      $
��Auto �6'   �� 5S      $��* �6'   .+�5V      �� -   5         ��Name$ SelectPatternComboBox��5V   �   $��1��6   $��Center=�5X        ComboBoxStyle#��[ 5Y      ��5V   +   �� # [  $ GridColumnSpan� �� UpdateSourceTrigger$ PropertyChanged=���5W      �� # [  $ Patterns�$ PropertyChanged=� �� NotifyOnTargetUpdated . 5[      �� 5]      -   5        $# SelectAdditionPatternComboBox��5]   +   $��3��6   $��Center=�5_      #��[ 5`      ��5^      �� # [  $ AdditionPatterns�$ PropertyChanged=�y�5a      �� # [  $ AdditionPatternVisibility�$ PropertyChanged=�5b      5d      5e      5f      5g      