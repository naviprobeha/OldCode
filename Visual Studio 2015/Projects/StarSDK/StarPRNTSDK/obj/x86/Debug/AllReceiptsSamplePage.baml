   M S B A M L   `   `   `  ����F  BStarPRNTSDK, Version=5.4.1.0, Culture=neutral, PublicKeyToken=null)clr-namespace:StarPRNTSDKStarPRNTSDK  5      O Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem 5      '    !StarPRNTSDK.AllReceiptsSamplePage   5      R NWindowsBase, Version=3.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=3.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=3.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      8x,http://schemas.microsoft.com/winfx/2006/xaml    5      Hmc;http://schemas.openxmlformats.org/markup-compatibility/2006    5      >d2http://schemas.microsoft.com/expression/blend/2008    5      )localclr-namespace:StarPRNTSDK    5      4sys&clr-namespace:System;assembly=mscorlib    5      -   5          N� Title$  AllReceiptsSamplePage��5
       � 	Resources 5      (   "StarPRNTSDK.AllReceiptsInfoManager   AllReceiptsInfoManager?   9StarPRNTSDK.PrintLocalizeReceiptWithAllReceiptsClickEvent * &PrintTextReceiptWithAllReceiptsCommand  TextReceipt    ReceiptInformationManager   StarPRNTSDK.BaseCommand   
Executable �� Source �� Path �� Mode . *PrintTextReceiptUTF8WithAllReceiptsCommand  TextReceiptUTF8 , (PrintRasterReceiptWithAllReceiptsCommand  RasterReceipt 5 1PrintRasterReceiptBothScaleWithAllReceiptsCommand  RasterReceiptBothScale 1	 -PrintRasterReceiptScaleWithAllReceiptsCommand 
 RasterReceiptScale3   -StarPRNTSDK.AllReceiptsRegistrationClickEvent " AllReceiptsRegistrationCommand2   ,StarPRNTSDK.DeleteRegistrationFileClickEvent , (AllReceiptsDeleteRegistrationFileCommand5   /StarPRNTSDK.GoNextPageWithSetReceiptTypeCommand # GoAllReceiptsExtPageTextCommand   ReceiptType#   StarPRNTSDK.GoNextPageCommand   NavigationUri ' #GoAllReceiptsExtPageTextUTF8Command % !GoAllReceiptsExtPageRasterCommand . *GoAllReceiptsExtPageRasterBothScaleCommand * &GoAllReceiptsExtPageRasterScaleCommand��%X  &	        &	      7  7   &	 _     7  7   &	 �     7  7   &	 �     7  7   &		 K    7 
 7   &	 �    &	 �    7   &	 �    7   &	 t    7   &	 �    7   &	 �    7   &	     7     5   
   6'     5   
   8    6�   �� 8  $ TextReceiptEnabled�$ OneWay=�  5   
   8    6�   �� 8  $ TextUTF8ReceiptEnabled�$ OneWay=�  5   
   8    6�   �� 8  $ RasterReceiptEnabled�$ OneWay=�  5   
   8    6�   �� 8  $ RasterReceiptEnabled�$ OneWay=�  5   
   8    6�   �� 8  $ RasterReceiptEnabled�$ OneWay=�  5   
    . 6Y     5   
    6b   �� 8   $ IsRegistered�$ OneWay=�  5   
   $
 Text=�6\   $# AllReceiptsExtSamplePage.xaml?�6o    6�   �� 8   $ TextReceiptEnabled�$ OneWay=�  5   
   $ TextUTF8=�6`   $# AllReceiptsExtSamplePage.xaml?�6w    6�   �� 8   $ TextUTF8ReceiptEnabled�$ OneWay=�  5   
   $ Raster=�6^   $# AllReceiptsExtSamplePage.xaml?�6s    6�   �� 8   $ RasterReceiptEnabled�$ OneWay=�  5   
   $ RasterBothScale=�6g   $# AllReceiptsExtSamplePage.xaml?�6�    6�   �� 8   $ RasterReceiptEnabled�$ OneWay=�  5   
   $ RasterScale=�6c   $# AllReceiptsExtSamplePage.xaml?�6}    6�   �� 8   $ RasterReceiptEnabled�$ OneWay=�5      .��5       $   StarPRNTSDK.CustomScrollViewer  .��5!   
   �� 	 �� Orientation$	 Vertical=�6   .+�5"      ��   PageTitleStyle#��[ 6q   ��6   ��   ResourceManager# [ $  AllReceiptsSamplePageTitle�6q   �� 5$        ListBoxSeparatorStyle#��[ 6k   ��6   �� # [ $ StarIOSDKSampleTitle�6k   �� 5&        ListBoxItemTemplate#��[ 6     ListBoxStyle#��[ 6a     ListBoxItemStyle#��[ 6�   ��6J   Z� .@�5'      !	   StarPRNTSDK.BaseListBoxItem	  
 	  ForeGroundColor

 ��  � 	  BorderBlushColor
 �����  	  ListBoxItemTouchedCommand# [ 5+       	  Title 5(      �� # [ $ Description�5+      	  5-      

 ��  �
 �����# [ 51       5.      �� # [ $ Description�51      	  53      

 ��  �
 �����# [ 57       54      �� # [ $ Description�57      	  59      

 ��  �
 �����# [ 5=       5:      �� # [ $ Description�5=      	  5?      

 ��  �# [	 5B       5@      �� # [
 $ Description�5B      5D      �� 5F      #��[ 6r   ��6   �� # [ $! StarIOExtManagerSampleTitle�6r   �� 5H      #��[ 6   #��[ 6a   #��[ 6�   ��6J   Z� .@�5I      	  

 ��  �
 �����# [ 5M       5J      �� # [ $ Description�5M      	  5O      

 ��  �
 �����# [ 5S       5P      �� # [ $ Description�5S      	  5U      

 ��  �
 �����# [ 5Y       5V      �� # [ $ Description�5Y      	  5[      

 ��  �
 �����# [ 5_       5\      �� # [ $ Description�5_      	  5a      

 ��  �# [ 5d       5b      �� # [
 $ Description�5d      5e      �� 5g      $��	Procedure��6   #��[ 6(   �� 5i      #��[ 6   #��[ 6a   #��[ 6�   ��6J   Z� .@�5j      	  

 ��  � 	  BackGroudColor
 ����
 �����# [ 5p       5k      �� # [  $" RegistrationStateDescription� 	  IsAnimation 5o      �� # [  $ IsUnRegistered�5p      	  5r      $ Delete Config File��5s      

 ��  �
 ����# [ 5v      5x      �� 5z      $��Print Setting��6   #��[ 6,   �� 5|       # ListBoxItemWithCheckBoxTemplate#��[ 6   #��[ 6m   #��[ 6�   ��6V   Z� .@�5}      )
   #StarPRNTSDK.ListBoxItemWithCheckBox
  $ Receipt��5~      

 ��  �
 ��ح�
 ����� 5�      Z�  
  CheckBoxIsChecked 5�      �� # [  $ PrintReceipt�$ TwoWay=�
  5�      $ Information��5�      

 ��  �
 ��ح�
 ����� 5�      Z�  5�      �� # [  $ PrintInformation�$ TwoWay=�
  5�      $ QR Code��5�      

 ��  �
 ��ح�
 ����� 5�      Z�  5�      �� # [  $ PrintQrCode�$ TwoWay=�5�      5�      5�      5�      