   M S B A M L   `   `   `  ����B  >TME_App, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null!clr-namespace:TME_AppTME_App  5   	   5clr-namespace:TME_App.VievModelTME_App.VievModel  5   	       TME_App.MainWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   'localclr-namespace:TME_App     5   	   .mwclr-namespace:TME_App.VievModel     5   	   T PMahApps.Metro, Version=2.0.0.0, Culture=neutral, PublicKeyToken=51482d6f650b2b3f6mah,http://metro.mahapps.com/winfx/xaml/controls  5   	     � Title$  
MainWindow��5
   	   $	��450��6   $	��800��6)    � DataContext 5      (   "TME_App.VievModel.ViewModelTME_App  5   
   .��5      � .+�5   
   �� -   5         ��Name$ TextBox1��5      $
��Left=�6$   $��
73,285,0,0q�6?    �� TextWrapping$
 Wrap=�6S   $	��Top=�6�   $	��155��6�   $��29��6�   ��6g   �� *	inputText+ �� Mode$ TwoWay=� �� UpdateSourceTrigger$ PropertyChanged=�6�   �� 5   
   $
��Left=�6   $��74��6/   $��	73,33,0,0q�6;   $
��Wrap=�6N   $	��Top=�6b   $	��248��6z   $����Naciśnij poniższy przycisk aby wczytać z bazy wcześniej wylosowane numery, a następnie poczekaj chwilę aż pojawi się informacja o dotychczasowym zapełnieniu bazy danych��6�   �� 5   
   $
��Left=�6   $��
72,148,0,0q�6/   $
��Wrap=�6C   $	��Top=�6�   $	��546��6�   $	��133��6�    J� RenderTransformOrigin$ 0.5,0.5&�6�   ��6W   �� *$"quantityOfNumbersYouCanRandMessage+$ OneWay=�$ PropertyChanged=�z�5      V� 5      .~�5      �� �� 5       �� AngleX$ -1.469Q�6$   �� 5      T� 5       T� X$ 0.308Q�6)   5      5      �� 5   
   -   5        	 ��Name$	 Button1��5      $#��Losuj liczby i zapisz do bazy��6"   $
��Left=�6J   $��
73,330,0,0q�6e   $	��Top=�6y   ��6�   �� *InputTextToInt+�� 5   
   -   5        $	 Button2��5      $��Wczytaj dane z bazy��6"   $
��Left=�6@   $��
73,108,0,0q�6[   $	��Top=�6o   ��6�   �� *LoadDatabase+6�   -  'MahApps.Metro.Controls.MetroProgressBar  5   
   -   5        
  Name$
 ProgressBar2��5      $��Center=�65   $��22��6R   $��	0,370,0,0q�6^   $	��Top=�6q   $	��730��6�   ���	#1F1316ED���	#FF00EAEF � Value 6�   �� *progressBarValue+$ OneWay=�5!      5"      