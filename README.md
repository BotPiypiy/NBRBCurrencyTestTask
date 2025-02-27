# NBRBCurrencyTestTask
Test task for ELogistics
Tools/Technologies: .Net Framework 4.7.2, MVVM pattern, WPF, NBRBApi.
Task: obtaining a list of available foreign currencies for a specified period;
      writing the received data to a text file in json format;
      reading from this file with displaying on the screen in a dynamic list;
      ability to edit the rate field or abbreviation of the selected line (the changed data is written to the desired point of the source file).

The list should contain the following fields in the following sequence: Date - date on which the rate is shown, Abbreviation - alphabetic code, Name - name of currency in Russian, OfficialRate - rate. If there is no data on the rate, output an empty field

About: Update changes into file is the same as Save operation, bcs other way will be more complex, slower and take more in app memory
