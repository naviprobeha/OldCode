   M S B A M L   `   `   `  ����F  BStarPRNTSDK, Version=5.4.1.0, Culture=neutral, PublicKeyToken=null)clr-namespace:StarPRNTSDKStarPRNTSDK  5      #    StarPRNTSDK.DisplaySamplePage   5      R NWindowsBase, Version=3.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=3.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=3.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      8x,http://schemas.microsoft.com/winfx/2006/xaml    5      Hmc;http://schemas.openxmlformats.org/markup-compatibility/2006    5      >d2http://schemas.microsoft.com/expression/blend/2008    5      )localclr-namespace:StarPRNTSDK    5        N� Title$  DisplaySamplePage��5	       � 	Resources 5      /   )StarPRNTSDK.CallDisplayFunctionClickEvent   CheckStatusCommand  CheckStatusFunction   Function   StarPRNTSDK.BaseCommand   
Executable  TextCommand  TextFunction  GraphicCommand  GraphicFunction  TurnOnOffCommand  TurnOnOffFunction  CursorCommand 	 CursorFunction 
 ContrastCommand  ContrastFunction $  CharacterSetInternationalCommand % !CharacterSetInternationalFunction  CharacterSetCodePageCommand   CharacterSetCodePageFunction  UserDefinedCharacterCommand   UserDefinedCharacterFunction#   StarPRNTSDK.GoNextPageCommand ! GoDisplayExtSamplePageCommand   NavigationUri��%�  &	        7  &	      7  &	 >     7  &	 ]     7  &	 |     7 	 &	
 �     7  &	 �     7  &	 �     7  &	 �     7  &	       5   
   8    . 6y     5   
   8    . 6k     5   
   8    . 6q     5   
   8    . 6u     5   
   8    . 6o     5   
   8    . 6s     5   
   8    . 6�     5   
   8    . 6�     5   
   8    . 6�     5   
   $ DisplayExtSamplePage.xaml?�6H    . 6r   5      .��5      $   StarPRNTSDK.CustomScrollViewer  .��5   
   ��  �� Orientation$ Vertical=�6   .+�5      ��   PageTitleStyle#��[ 6m   ��6   ��   ResourceManager �� Source# [  �� Path$ DisplaySamplePageTitle�6m   �� 5      $��Contents��6     ListBoxSeparatorStyle#��[ 6'   �� 5        ListBoxItemTemplate#��[ 6     ListBoxStyle#��[ 6a     ListBoxItemStyle#��[ 6�   ��6J   Z� .@�5       !   StarPRNTSDK.BaseListBoxItem     ForeGroundColor
 ��  �	   BorderBlushColor
	 ����� 
   ListBoxItemTouchedCommand#
 [  5$         Title 5!      �� # [ $ Title�5$        5&      
 ��  �
	 �����#
 [ 5*       5'      �� # [ $ Title�5*        5,      
 ��  �
	 �����#
 [ 50       5-      �� # [ $ Title�50        52      
 ��  �
	 �����#
 [ 56       53      �� # [ $ Title�56        58      
 ��  �
	 �����#
 [ 5<       59      �� # [	 $ Title�5<        5>      
 ��  �
	 �����#
 [
 5B       5?      �� # [ $ Title�5B        5D      
 ��  �
	 �����#
 [ 5H       5E      �� # [ $ Title�5H        5J      
 ��  �
	 �����#
 [ 5N       5K      �� # [ $ Title�5N        5P      
 ��  �#
 [ 5S       5Q      �� # [ $ Title�5S      5U      �� 5W      #��[ 6r   ��6   �� # [ $! StarIOExtManagerSampleTitle�6r   �� 5Y      #��[ 6   #��[ 6a   #��[ 6�   ��6J   Z� .@�5Z        $ Sample��5[      
 ��  �
	 �����#
 [ 5^      5a      5c      5d      5e      