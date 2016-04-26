package com.example.reynhart.myapplication;

import android.app.ProgressDialog;
import android.content.Intent;
import android.graphics.Color;
import android.os.AsyncTask;
import android.os.Bundle;
import android.support.design.widget.FloatingActionButton;
import android.support.design.widget.Snackbar;
import android.support.v7.app.AppCompatActivity;
import android.support.v7.widget.GridLayoutManager;
import android.support.v7.widget.Toolbar;
import android.text.InputType;
import android.text.method.PasswordTransformationMethod;
import android.util.JsonWriter;
import android.util.Log;
import android.view.View;
import android.view.Menu;
import android.view.MenuItem;
import android.view.ViewGroup;
import android.widget.ArrayAdapter;
import android.widget.CheckBox;
import android.widget.EditText;
import android.widget.GridLayout;
import android.widget.LinearLayout;
import android.widget.RadioButton;
import android.widget.RadioGroup;
import android.widget.RelativeLayout;
import android.widget.Spinner;
import android.widget.TableLayout;
import android.widget.TableRow;
import android.widget.TextView;

import org.json.JSONArray;
import org.json.JSONException;
import org.json.JSONObject;

import java.io.BufferedInputStream;
import java.io.BufferedReader;
import java.io.IOException;
import java.io.InputStream;
import java.io.InputStreamReader;
import java.io.OutputStream;
import java.io.OutputStreamWriter;
import java.io.Reader;
import java.net.HttpURLConnection;
import java.net.URL;
import java.text.SimpleDateFormat;
import java.util.ArrayList;
import java.util.Calendar;
import java.util.List;



import java.net.HttpURLConnection;
import java.util.logging.Level;

public class MyActivity extends AppCompatActivity {

    public final static String EXTRA_MESSAGE = "com.example.reynhart.myapplication.MESSAGE";

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_my);

        new GetData().execute("");
    }

    @Override
    public boolean onCreateOptionsMenu(Menu menu) {
        // Inflate the menu; this adds items to the action bar if it is present.
        getMenuInflater().inflate(R.menu.menu_my, menu);
        return true;
    }

    @Override
    public boolean onOptionsItemSelected(MenuItem item) {
        // Handle action bar item clicks here. The action bar will
        // automatically handle clicks on the Home/Up button, so long
        // as you specify a parent activity in AndroidManifest.xml.
        int id = item.getItemId();

        //noinspection SimplifiableIfStatement
        if (id == R.id.action_settings) {
            return true;
        }

        return super.onOptionsItemSelected(item);
    }

    /** Called when the user clicks the Send button */
    public void sendMessage(View view) {

    }

    private class GetData extends AsyncTask<String, String, String> {

        HttpURLConnection urlConnection;
        private ProgressDialog pDialog;
        String result;

        @Override
        protected void onPreExecute() {
            super.onPreExecute();
            pDialog = new ProgressDialog(MyActivity.this);
            pDialog.setMessage("Getting Data ...");
            pDialog.setIndeterminate(false);
            pDialog.setCancelable(true);
            pDialog.show();

        }

        @Override
        protected String doInBackground(String... args) {
            try {
                URL url = new URL("http://192.168.2.105/rekadia-template/JSONParser/Parse?url=1");
                urlConnection = (HttpURLConnection) url.openConnection();
                urlConnection.setRequestProperty("Content-Type", "application/json");

                /* optional request header */
                urlConnection.setRequestProperty("Accept", "application/json");
                urlConnection.setRequestMethod("GET");
                int statusCode = urlConnection.getResponseCode();

                if(statusCode == 200)
                {
                    InputStream in = new BufferedInputStream(urlConnection.getInputStream());

                    result = convertInputStreamToString(in);
                }
                else
                {
                    result = "error";
                }



            }catch( Exception e) {
                e.printStackTrace();
            }
            finally {
                urlConnection.disconnect();
            }


            return result;
        }

        @Override
        protected void onPostExecute(String result) {
            pDialog.dismiss();
            TableLayout lyt = (TableLayout) findViewById(R.id.layout1);
            String data = result;

            try
            {
                JSONArray jsonArray = new JSONArray(data);
                String rowNumber = null;
                String type = null;
                String value = null;
                String id = null;
                String prevId = null;
                String name= null;
                for(int i=0; i<jsonArray.length(); i++)
                {
                    JSONObject rowData = jsonArray.getJSONObject(i);
                    JSONArray rowContentArray = new JSONArray(rowData.getString("Content"));
                    rowNumber = rowData.getString("RowNumber");

//                    LinearLayout subLayout = new LinearLayout(MyActivity.this);
//
//                    subLayout.setLayoutParams(new LinearLayout.LayoutParams(LinearLayout.LayoutParams.MATCH_PARENT, LinearLayout.LayoutParams.WRAP_CONTENT));
//                    subLayout.setOrientation(LinearLayout.HORIZONTAL);
//                    GridLayout subLayout = new GridLayout(MyActivity.this);
//                    subLayout.setLayoutParams(new GridLayoutManager.LayoutParams(GridLayoutManager.LayoutParams.MATCH_PARENT,GridLayoutManager.LayoutParams.WRAP_CONTENT));
//                    subLayout.setColumnCount(rowContentArray.length());

                    TableRow.LayoutParams rowParams = new TableRow.LayoutParams(TableRow.LayoutParams.WRAP_CONTENT, TableRow.LayoutParams.WRAP_CONTENT);
                    TableRow tableRow = new TableRow(MyActivity.this);
                    tableRow.setLayoutParams(rowParams);

                    lyt.addView(tableRow);

                    for(int j=0; j<rowContentArray.length(); j++)
                    {
                        JSONObject rowContentData = rowContentArray.getJSONObject(j);
                        type = rowContentData.getString("Type");
                        value = rowContentData.getString("Value");
                        id = rowContentData.getString("Id");
                        name = rowContentData.getString("Name");

                        switch(type)
                        {
                            case "label":
                                TextView textView = new TextView(MyActivity.this);
//                                RelativeLayout.LayoutParams paramsLabel = new RelativeLayout.LayoutParams(RelativeLayout.LayoutParams.WRAP_CONTENT,
//                                        RelativeLayout.LayoutParams.WRAP_CONTENT);
//                                paramsLabel.addRule(RelativeLayout.ALIGN_PARENT_BOTTOM,RelativeLayout.TRUE);

//                                LinearLayout.LayoutParams paramsLabel = new LinearLayout.LayoutParams(LinearLayout.LayoutParams.WRAP_CONTENT,
//                                        LinearLayout.LayoutParams.WRAP_CONTENT);
//                                paramsLabel.weight=1;

//                                GridLayoutManager.LayoutParams paramsLabel = new GridLayoutManager.LayoutParams(GridLayoutManager.LayoutParams.WRAP_CONTENT,GridLayoutManager.LayoutParams.WRAP_CONTENT);
//
                                TableRow.LayoutParams paramsLabel = new TableRow.LayoutParams(TableRow.LayoutParams.WRAP_CONTENT, TableRow.LayoutParams.WRAP_CONTENT);


                                textView.setLayoutParams(paramsLabel);
                                textView.setText(value);
                                tableRow.addView(textView);

                                break;
                            case "text":
                                EditText editText = new EditText(MyActivity.this);
//                                RelativeLayout.LayoutParams paramsText = new RelativeLayout.LayoutParams(RelativeLayout.LayoutParams.MATCH_PARENT,
//                                        RelativeLayout.LayoutParams.WRAP_CONTENT);
//                                paramsText.addRule(RelativeLayout.ALIGN_PARENT_BOTTOM,RelativeLayout.TRUE);

//                                LinearLayout.LayoutParams paramsText = new LinearLayout.LayoutParams(LinearLayout.LayoutParams.MATCH_PARENT,
//                                        LinearLayout.LayoutParams.WRAP_CONTENT);
//                                paramsText.weight=1;

//                                GridLayoutManager.LayoutParams paramsText = new GridLayoutManager.LayoutParams(GridLayoutManager.LayoutParams.MATCH_PARENT,GridLayoutManager.LayoutParams.WRAP_CONTENT);

                                TableRow.LayoutParams paramsText = new TableRow.LayoutParams(TableRow.LayoutParams.MATCH_PARENT, TableRow.LayoutParams.WRAP_CONTENT);
                                if(rowContentArray.length() == 2)
                                {
                                    paramsText.span = 4;
                                }

                                editText.setLayoutParams(paramsText);
                                tableRow.addView(editText);

                                break;
                            case "password":
                                EditText passwordText = new EditText(MyActivity.this);
//                                RelativeLayout.LayoutParams paramsPassword = new RelativeLayout.LayoutParams(RelativeLayout.LayoutParams.MATCH_PARENT,
//                                        RelativeLayout.LayoutParams.WRAP_CONTENT);
//                                paramsPassword.addRule(RelativeLayout.ALIGN_PARENT_BOTTOM,RelativeLayout.TRUE);

//                                LinearLayout.LayoutParams paramsPassword = new LinearLayout.LayoutParams(LinearLayout.LayoutParams.MATCH_PARENT,
//                                        LinearLayout.LayoutParams.WRAP_CONTENT);
//                                paramsPassword.weight=1;

//                                GridLayoutManager.LayoutParams paramsPassword = new GridLayoutManager.LayoutParams(GridLayoutManager.LayoutParams.MATCH_PARENT,GridLayoutManager.LayoutParams.WRAP_CONTENT);

                                TableRow.LayoutParams paramsPassword = new TableRow.LayoutParams(TableRow.LayoutParams.MATCH_PARENT, TableRow.LayoutParams.WRAP_CONTENT);

                                passwordText.setLayoutParams(paramsPassword);
                                passwordText.setInputType(InputType.TYPE_TEXT_VARIATION_PASSWORD);
                                passwordText.setTransformationMethod(PasswordTransformationMethod.getInstance());
                                tableRow.addView(passwordText);

                                break;
                            case "radioGroup":
                                    JSONArray radioGroupContent = rowContentData.getJSONArray("RadioGroupModel");
                                    RadioGroup rg = new RadioGroup(MyActivity.this); //create the RadioGroup
                                    rg.setOrientation(RadioGroup.VERTICAL);//or RadioGroup.VERTICAL


                                RadioGroup.LayoutParams rprms;

                                for(int k=0;k<radioGroupContent.length();k++){
                                    JSONObject radioContent = radioGroupContent.getJSONObject(k);

                                    RadioButton radioButton = new RadioButton(MyActivity.this);
                                    radioButton.setText(radioContent.getString("InnerLabel"));
//                                    radioButton.setId("rbtn"+k);
                                    rprms= new RadioGroup.LayoutParams(RadioGroup.LayoutParams.WRAP_CONTENT, RadioGroup.LayoutParams.WRAP_CONTENT);
                                    rg.addView(radioButton, rprms);
                                }

                                tableRow.addView(rg);

                                break;
                            case "checkBoxList":
                                JSONArray checkBoxListContent = rowContentData.getJSONArray("CheckBoxListModel");
                                LinearLayout cbl = new LinearLayout(MyActivity.this);
                                cbl.setOrientation(LinearLayout.VERTICAL);

                                LinearLayout.LayoutParams cblp;

                                for(int k=0;k<checkBoxListContent.length();k++){
                                    JSONObject checkBoxContent = checkBoxListContent.getJSONObject(k);

                                    CheckBox cb = new CheckBox(getApplicationContext());
                                    cb.setText(checkBoxContent.getString("InnerLabel"));
                                    cb.setTextColor(Color.BLACK);
                                    cblp= new LinearLayout.LayoutParams(LinearLayout.LayoutParams.WRAP_CONTENT, LinearLayout.LayoutParams.WRAP_CONTENT);
                                    cbl.addView(cb,cblp);
//                                    RadioButton radioButton = new RadioButton(MyActivity.this);
//                                    radioButton.setText(radioContent.getString("InnerLabel"));
////                                    radioButton.setId("rbtn"+k);
//                                    rprms= new RadioGroup.LayoutParams(RadioGroup.LayoutParams.WRAP_CONTENT, RadioGroup.LayoutParams.WRAP_CONTENT);
//                                    rg.addView(radioButton, rprms);
                                }

                                tableRow.addView(cbl);

                                break;
                            case "select":
                                Spinner spinner = new Spinner(getApplicationContext());
                                List<String> list = new ArrayList<String>();
                                JSONArray optionContent = rowContentData.getJSONArray("OptionValue");

                                for(int k=0;k<optionContent.length();k++) {
                                    JSONObject optionValue = optionContent.getJSONObject(k);
                                    list.add(optionValue.getString("Text"));
                                }


                                ArrayAdapter<String> spinnerArrayAdapter = new ArrayAdapter<String>(getApplicationContext(),android.R.layout.simple_spinner_item, list);
                                spinnerArrayAdapter.setDropDownViewResource(android.R.layout.simple_spinner_dropdown_item); // The drop down view
                                spinner.setAdapter(spinnerArrayAdapter);



//                                Spinner.LayoutParams spinnerLayoutParams = new Spinner.LayoutParams(Spinner.LayoutParams.WRAP_CONTENT,Spinner.LayoutParams.WRAP_CONTENT);
//                                spinner.setLayoutParams(spinnerLayoutParams);

                                tableRow.addView(spinner);

                                break;
                            case "hidden":
                                EditText hiddenText = new EditText(MyActivity.this);
//                                RelativeLayout.LayoutParams paramsText = new RelativeLayout.LayoutParams(RelativeLayout.LayoutParams.MATCH_PARENT,
//                                        RelativeLayout.LayoutParams.WRAP_CONTENT);
//                                paramsText.addRule(RelativeLayout.ALIGN_PARENT_BOTTOM,RelativeLayout.TRUE);

//                                LinearLayout.LayoutParams paramsText = new LinearLayout.LayoutParams(LinearLayout.LayoutParams.MATCH_PARENT,
//                                        LinearLayout.LayoutParams.WRAP_CONTENT);
//                                paramsText.weight=1;

//                                GridLayoutManager.LayoutParams paramsText = new GridLayoutManager.LayoutParams(GridLayoutManager.LayoutParams.MATCH_PARENT,GridLayoutManager.LayoutParams.WRAP_CONTENT);

                                TableRow.LayoutParams paramsHiddenText = new TableRow.LayoutParams(TableRow.LayoutParams.MATCH_PARENT, TableRow.LayoutParams.WRAP_CONTENT);


                                hiddenText.setLayoutParams(paramsHiddenText);
                                hiddenText.setText(value);
                                hiddenText.setVisibility(View.GONE);
                                tableRow.addView(hiddenText);

                                break;
                        }

                    }

                }
            }
            catch(JSONException e)
            {
                System.out.print(e.toString());
            }
        }

        private String convertInputStreamToString(InputStream inputStream) throws IOException {
            BufferedReader bufferedReader = new BufferedReader( new InputStreamReader(inputStream));
            String line = "";
            String result = "";
            while((line = bufferedReader.readLine()) != null){
                result += line;
            }

            /* Close Stream */
            if(null!=inputStream){
                inputStream.close();
            }
            return result;
        }

    }


}





