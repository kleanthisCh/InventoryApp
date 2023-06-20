package gr.chanioglou.inventory;

import androidx.activity.result.ActivityResultLauncher;
import androidx.appcompat.app.AlertDialog;
import androidx.appcompat.app.AppCompatActivity;

import android.content.DialogInterface;
import android.os.Bundle;
import android.util.Log;
import android.view.View;
import android.widget.Button;
import android.widget.TextView;
import android.widget.Toast;

import com.android.volley.Request;
import com.android.volley.RequestQueue;
import com.android.volley.Response;
import com.android.volley.VolleyError;
import com.android.volley.toolbox.JsonObjectRequest;
import com.android.volley.toolbox.Volley;
import com.journeyapps.barcodescanner.CaptureActivity;
import com.journeyapps.barcodescanner.ScanContract;
import com.journeyapps.barcodescanner.ScanOptions;

import org.json.JSONArray;
import org.json.JSONException;
import org.json.JSONObject;

public class ScanActivity extends AppCompatActivity {
    private Button btn_add;
    private Button btn_sub;
    private TextView text_barcode;
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_scan);

        Bundle bundle = getIntent().getExtras();
        String message = bundle.getString("message");


        btn_add = findViewById(R.id.btn_add);
        btn_sub = findViewById(R.id.btn_sub);
        text_barcode = findViewById(R.id.text_barcode);

        text_barcode.setText(message);

        btn_sub.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                if (text_barcode.getText().toString().trim().isEmpty()){
                    text_barcode.setError("Please scan a barcode");
                }
                else{
                    Toast.makeText(ScanActivity.this, text_barcode.getText().toString().trim(), Toast.LENGTH_SHORT).show();
                    RequestQueue requestQueue = Volley.newRequestQueue(ScanActivity.this);
                    String url = "https://localhost:45458/api/Barcodes/SubOne/"+ text_barcode.getText().toString().trim();

                    JsonObjectRequest jsonObjectRequest = new JsonObjectRequest(Request.Method.PUT, url, null,new Response.Listener<JSONObject>(){
                        @Override
                        public void onResponse(JSONObject response) {

                            Toast.makeText(ScanActivity.this, response.toString(), Toast.LENGTH_SHORT).show();
                        }
                    },new Response.ErrorListener() {
                        @Override
                        public void onErrorResponse(VolleyError error) {
                            Toast.makeText(ScanActivity.this, error.toString(), Toast.LENGTH_SHORT).show();
                        }
                    });

                    requestQueue.add(jsonObjectRequest);
                }
            }
        });


        btn_add.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                if (text_barcode.getText().toString().trim().isEmpty()){
                    text_barcode.setError("Please scan a barcode");
                }
                else{
                    Toast.makeText(ScanActivity.this, text_barcode.getText().toString().trim(), Toast.LENGTH_SHORT).show();
                    RequestQueue requestQueue = Volley.newRequestQueue(ScanActivity.this);
                    String url = "https://localhost:45458/api/Barcodes/AddOne/"+ text_barcode.getText().toString().trim();

                    JsonObjectRequest jsonObjectRequest = new JsonObjectRequest(Request.Method.PUT, url, null,new Response.Listener<JSONObject>(){
                        @Override
                        public void onResponse(JSONObject response) {
                            Toast.makeText(ScanActivity.this, response.toString(), Toast.LENGTH_SHORT).show();
                        }
                    },new Response.ErrorListener() {
                        @Override
                        public void onErrorResponse(VolleyError error) {
                            Toast.makeText(ScanActivity.this, error.toString(), Toast.LENGTH_SHORT).show();
                        }
                    });

                    requestQueue.add(jsonObjectRequest);
                }
            }
        });
    }


}