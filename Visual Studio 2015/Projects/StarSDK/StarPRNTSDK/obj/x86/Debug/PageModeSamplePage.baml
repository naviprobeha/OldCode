   M S B A M L   `   `   `  ����F  BStarPRNTSDK, Version=5.4.1.0, Culture=neutral, PublicKeyToken=null)clr-namespace:StarPRNTSDKStarPRNTSDK  5      $    StarPRNTSDK.PageModeSamplePage   5      R NWindowsBase, Version=3.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=3.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=3.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      8x,http://schemas.microsoft.com/winfx/2006/xaml    5      Hmc;http://schemas.openxmlformats.org/markup-compatibility/2006    5      >d2http://schemas.microsoft.com/expression/blend/2008    5      )localclr-namespace:StarPRNTSDK    5        N� Title$  PageModeSamplePage��5	       � 	Resources 5      +   %StarPRNTSDK.ReceiptInformationManager   TextRotation0   Type   Rotation   Language  ReceiptInformationManager �� Source �� Path   	PaperSize  TextRotation90  TextRotation180  TextRotation270<   6StarPRNTSDK.PrintLocalizeReceiptWithPageModeClickEvent $  PrintTextReceiptRotation0Command    ReceiptInformationManager   StarPRNTSDK.BaseCommand	   
Executable % !PrintTextRexeiptRotation90Command & "PrintTextRexeiptRotation180Command & "PrintTextRexeiptRotation270Command��%�  &	        7  7  &	      7  7  &	 �     7  7  &	 �    7  7  &	      7   &	     7  &	 >    7  &	 ]    7    5   
   $ 	TextLabel=�6@   $ Normal=�6�    6Q   �� 8   $ Language� 6�   �� 8  $ 	PaperSize�6�     5   
   $ 	TextLabel=�6A   $ Right90=�6    6R   �� 8   $ Language� 6�   �� 8  $ 	PaperSize�6     5   
   $ 	TextLabel=�6B   $ 	Rotate180=�6   6S   �� 8   $ Language� 6�   �� 8  $ 	PaperSize�6    5   
   $ 	TextLabel=�6B   $ Left90=�6   6S   �� 8   $ Language� 6�   �� 8  $ 	PaperSize�6    5   
   8   	 . 6�     5   
   8   	 . 6�     5   
   8   	 . 6�     5   
   8   	 . 6�   5      .��5      $   StarPRNTSDK.CustomScrollViewer  .��5   
   �� 
 �� Orientation$
 Vertical=�6   .+�5      ��  	 PageTitleStyle#��[	 6n   ��6   ��  
 ResourceManager# [
 $ PageModeSamplePageTitle�6n   �� 5        ListBoxSeparatorStyle#��[ 6k   ��6   �� # [
 $ StarIOSDKSampleTitle�6k   �� 5        ListBoxItemTemplate#��[ 6     ListBoxStyle#��[ 6a     ListBoxItemStyle#��[ 6�   ��6J   Z� .@�5      !   StarPRNTSDK.BaseListBoxItem     ForeGroundColor
 ��  �   BorderBlushColor
 �����    ListBoxItemTouchedCommand# [ 5#         Title 5       �� # [  $ DescriptionWithRotation�5#        5%      
 ��  �
 �����# [ 5)       5&      �� # [ $ DescriptionWithRotation�5)        5+      
 ��  �
 �����# [ 5/       5,      �� # [ $ DescriptionWithRotation�5/        51      
 ��  �
 �����# [ 55       52      �� # [ $ DescriptionWithRotation�55      58      5<      5=      5>      