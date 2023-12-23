var host = "https://localhost:";
var port = "7021/";
var CarEndpoint = "api/Car/";
var CarManufacturerEndpoint = "api/CarManufacturer/";
var loginEndpoint = "api/authentication/login";
var registerEndpoint = "api/authentication/register";
var filterCar = "api/filter";
var formAction = "Create";
var editingId;
var jwt_token;


function showRegistration() {
	document.getElementById("loginFormDiv").style.display = "none";
	document.getElementById("registerFormDiv").style.display = "block";
	document.getElementById("logout").style.display = "none";
}
function showLogin() {
	document.getElementById("loginFormDiv").style.display = "block";
	document.getElementById("registerFormDiv").style.display = "none";
	document.getElementById("logout").style.display = "none";
}
function validateLoginForm(username, password) {
	if (username.length === 0) {
		alert("Username field can not be empty.");
		return false;
	} else if (password.length === 0) {
		alert("Password field can not be empty.");
		return false;
	}
	return true;
}
function validateRegisterForm(username, email, password, confirmPassword) {
	if (username.length === 0) {
		alert("Username field can not be empty.");
		return false;
	} else if (email.length === 0) {
		alert("Email field can not be empty.");
		return false;
	} else if (password.length === 0) {
		alert("Password field can not be empty.");
		return false;
	} else if (confirmPassword.length === 0) {
		alert("Confirm password field can not be empty.");
		return false;
	} else if (password !== confirmPassword) {
		alert("Password value and confirm password value should match.");
		return false;
	}
	return true;
}
function registerUser() {
	var username = document.getElementById("usernameRegister").value;
	var email = document.getElementById("emailRegister").value;
	var password = document.getElementById("passwordRegister").value;
	var confirmPassword = document.getElementById("confirmPasswordRegister").value;

	if (validateRegisterForm(username, email, password, confirmPassword)) {
		var url = host + port + registerEndpoint;
		var sendData = { "Username": username, "Email": email, "Password": password };
		fetch(url, { method: "POST", headers: { 'Content-Type': 'application/json' }, body: JSON.stringify(sendData) })
			.then((response) => {
				if (response.status === 200) {
					document.getElementById("registerForm").reset();
					console.log("Successful registration");
					alert("Successful registration");
					showLogin();
				} else {
					console.log("Error occured with code " + response.status);
					console.log(response);
					alert("Greksa prilikom registracije");
					response.text().then(text => { console.log(text); })
				}
			})
			.catch(error => console.log(error));
	}
	return false;
}
function loginUser() {
	var username = document.getElementById("usernameLogin").value;
	var password = document.getElementById("passwordLogin").value;

	if (validateLoginForm(username, password)) {
		var url = host + port + loginEndpoint;
		var sendData = { "Username": username, "Password": password };
		fetch(url, { method: "POST", headers: { 'Content-Type': 'application/json' }, body: JSON.stringify(sendData) })
			.then((response) => {
				if (response.status === 200) {
					document.getElementById("loginForm").reset();
					console.log("Successful login");
					alert("Successful login");
					response.json().then(function (data) {
						console.log(data);
						document.getElementById("info").innerHTML = "User: " + data.username + "<i/>";
						document.getElementById("logout").style.display = "block";
						document.getElementById("loginFormDiv").style.display = "none";
						document.getElementById("registerFormDiv").style.display = "none";
						jwt_token = data.token;
					});
				} else {
					console.log("Error occured with code " + response.status);
					console.log(response);
					alert("Greska prilikom prijave!");
					response.text().then(text => { console.log(text); })
				}
			})
			.catch(error => console.log(error));
	}       
	return false;
}
function loadPage() {
	loadCarManufacturerForDropdown();
	loadCars();
	document.getElementById("postCar").style.display = "none";
    document.getElementById("loginFormDiv").style.display = "none";
	document.getElementById("registerFormDiv").style.display = "none";
	document.getElementById("carFormDiv").style.display = "block";
	if(jwt_token){
	document.getElementById("logout").style.display = "block";
	}
	document.getElementById("detail").style.display = "none";
	
}

function logout() {
	loadCarManufacturerForDropdown();
	loadCars();
	jwt_token = undefined;
	document.getElementById("info").innerHTML = "";
	document.getElementById("data").innerHTML = "";
	document.getElementById("loginFormDiv").style.display = "block";
	document.getElementById("registerFormDiv").style.display = "none";
	document.getElementById("logout").style.display = "none";
	document.getElementById("btnRegister").style.display = "initial";
	document.getElementById("carFormDiv").style.display = "block";
}
function postCar() {
	document.getElementById("data").innerHTML = "";
	document.getElementById("loginFormDiv").style.display = "none";
	document.getElementById("registerFormDiv").style.display = "none";
	document.getElementById("logout").style.display = "block";
	document.getElementById("carFormDiv").style.display = "none";
	document.getElementById("btnRegister").style.display = "none";
	document.getElementById("postCar").style.display = "block";
	document.getElementById("Images").style.display = "block";
	document.getElementById("imageLabel").style.display = "block";
	
}
function showError() {
	var container = document.getElementById("data");
	container.innerHTML = "";

	var div = document.createElement("div");
	var h1 = document.createElement("h1");
	var errorText = document.createTextNode("Error occured while retrieving data!");

	h1.appendChild(errorText);
	div.appendChild(h1);
	container.append(div);
}

function loadCars() {

	var requestUrl = host + port + CarEndpoint;
	console.log("URL zahteva: " + requestUrl);
	fetch(requestUrl)
		.then((response) => {
			if (response.status === 200) {
				response.json().then(setCar);
			} else {
				console.log("Error occured with code " + response.status);
				showError();
			}
		})
		.catch(error => console.log(error));
}
function loadDetail() {
	document.getElementById("data").innerHTML = "";
	document.getElementById("loginFormDiv").style.display = "none";
	document.getElementById("registerFormDiv").style.display = "none";
	document.getElementById("logout").style.display = "none";
	document.getElementById("carFormDiv").style.display = "none";
	document.getElementById("btnRegister").style.display = "none";
	document.getElementById("postCar").style.display = "none";
	document.getElementById("detail").style.display = "block";
	
}
function setCar(data) {
    console.log(data);
    var container = document.getElementById("data");
    container.innerHTML = "";
    for (var i = 0; i < data.length; i++) {
        var carButton = createCarButton(data[i]);
		carButton.addEventListener("click", function () {
            loadDetail();
        });
        container.appendChild(carButton);
    }
}

function createCarButton(carData) {
    console.log(carData);
    var carButton = document.createElement("button");
    carButton.className = "car-button";

    var image = document.createElement("img");
	if(carData.images != null && carData.images.length > 0){
    image.src = host + port + carData.images[0].relativePath;
    image.className = "picImg";
	}
	

    var name = document.createElement("p");
    name.textContent = carData.name;

    carButton.appendChild(image);
    carButton.appendChild(name);
    carButton.addEventListener("click", function () {
		var requestUrl = host + port + CarEndpoint + carData.id;
	console.log("URL zahteva: " + requestUrl);
	fetch(requestUrl)
		.then((response) => {
			if (response.status === 200) {
				response.json().then(setCarDetail);
			} else {
				console.log("Error occured with code " + response.status);
				showError();
			}
		})
		.catch(error => console.log(error));
    });

    return carButton;
}
function setCarDetail(data){
	console.log(data);
    var container = document.getElementById("detail");
    container.innerHTML = "";
        var carButton = setCarDetailWithImages(data);
        container.appendChild(carButton);
}
function setCarDetailWithImages(carData) {
    console.log(carData);
    var container = document.getElementById("detail");
    container.innerHTML = "";

    var carDetailContainer = document.createElement("div");
    carDetailContainer.className = "car-detail-container";

    var imagesContainer = document.createElement("div");
    imagesContainer.className = "images-container";

    var images = carData.images;

    images.forEach((image, index) => {
        var img = document.createElement("img");
        img.src = host + port + image.relativePath;
        img.className = "detail-image";
        img.alt = "Car Image";
        imagesContainer.appendChild(img);

        if ((index + 1) % 3 === 0) {
            imagesContainer.appendChild(document.createElement("br"));
        }
    });

    carDetailContainer.appendChild(imagesContainer);

    var detailsContainer = document.createElement("div");
    detailsContainer.className = "details-container";
	detailsContainer.style.backgroundColor = "rgb(164, 191, 239)";
	detailsContainer.style.width = "500px";
	detailsContainer.style.marginBottom = "5px";

    var details = [
        { label: "Name", value: carData.name },
        { label: "Price", value: carData.price + " e" },
        { label: "Production Year", value: carData.productionYear },
        { label: "Vehicle Type", value: carData.vehicleType },
        { label: "Fuel", value: carData.fuel },
        { label: "Cubic Capacity", value: carData.cubicCapacity + " cm3" },
        { label: "Power", value: carData.power + " hp" },
        { label: "Color", value: carData.color },
        { label: "Mileage", value: carData.mileage + " km"},
        { label: "Transmission", value: carData.transmission },
		{ label: "Description", value: carData.description }
    ];

    details.forEach(detail => {
		var detailItem = document.createElement("div");
        detailItem.innerHTML = `<strong>${detail.label}:</strong> ${detail.value}`;
        detailsContainer.appendChild(detailItem);
    });

	if (jwt_token) {
		var deleteButton = document.createElement("button");
		deleteButton.className = "btn btn-danger";
		deleteButton.textContent = "DELETE CAR";
		deleteButton.addEventListener("click", function() {
			deleteCar(carData.id);
		});
		deleteButton.style.height = "50px";
		detailsContainer.appendChild(deleteButton);
	} 
	if (jwt_token) {
		var updateButton = document.createElement("button");
		updateButton.className = "btn btn-warning";
		updateButton.textContent = "UPDATE CAR";
		updateButton.addEventListener("click", function() {
			updateCar(carData.id);
		});
		updateButton.style.height = "50px";
		detailsContainer.appendChild(updateButton);
	} 

    carDetailContainer.appendChild(detailsContainer);
    container.appendChild(carDetailContainer);
}

function loadCarManufacturerForDropdown() {
	var requestUrl = host + port + CarManufacturerEndpoint;
	console.log("URL zahteva: " + requestUrl);
  
	var headers = {};
	if (jwt_token) {
	  headers.Authorization = 'Bearer ' + jwt_token;
	}
  
	fetch(requestUrl, { headers: headers })
	  .then((response) => {
		if (response.status === 200) {
		  response.json().then(function (data) {
			setCarManufacturerInDropdown(data, document.getElementById("carManufacturerId"));
			setCarManufacturerInDropdown(data, document.getElementById("carManufacturerIdPost"));
		  });
		} else {
		  console.log("Error occured with code " + response.status);
		}
	  })
	  .catch(error => console.log(error));
  };
  
  function setCarManufacturerInDropdown(data, dropdown) {
	dropdown.innerHTML = "";
	for (var i = 0; i < data.length; i++) {
	  var option = document.createElement("option");
	  option.value = data[i].id;
	  var text = document.createTextNode(data[i].name);
	  option.appendChild(text);
	  dropdown.appendChild(option);
	}
	console.log(dropdown);
  }

function handleTransmissionCheckboxChange(checkedCheckboxId) {

    const otherCheckboxId = checkedCheckboxId === 'manual' ? 'automatic' : 'manual';
    const otherCheckbox = document.getElementById(otherCheckboxId);
    otherCheckbox.checked = false;
}
function deleteCar(id) {
	var deleteID = id;

	var url = host + port + CarEndpoint + deleteID.toString();
	var headers = { 'Content-Type': 'application/json' };
	if (jwt_token) {
		headers.Authorization = 'Bearer ' + jwt_token;
	}

	fetch(url, { method: "DELETE", headers: headers})
		.then((response) => {
			if (response.status === 204) {
				console.log("Successful action");
				loadPage();
			} else {
				console.log("Error occured with code " + response.status);
				alert("Error occured!");
			}
		})
		.catch(error => console.log(error));
};
function updateCar(id) {
	
	var editId = id;
  
  var urlUpdate = host + port + CarEndpoint + editId.toString();
  var headers = {};
  
  if (jwt_token) {
  headers.Authorization = 'Bearer ' + jwt_token;
  }
  fetch(urlUpdate, { headers: headers})
  .then((response) => {
	  if (response.status === 200) {
		  console.log("Successful action");
		  response.json().then(data => {
				console.log(data);
				document.getElementById("postCar").style.display = "block";
				document.getElementById("name").value = data.name;
				document.getElementById("price").value = data.price;
				document.getElementById("type").value = data.vehicleType;
				document.getElementById("ProduYear").value = data.productionYear;
				document.getElementById("fuelPostCar").value = data.fuel;
				document.getElementById("cubicCapacity").value = data.cubicCapacity;
				document.getElementById("power").value = data.power;
				document.getElementById("color").value = data.color;
				document.getElementById("mileage").value = data.mileage;
				document.getElementById("description").value = data.description;
				document.getElementById("Images").style.display = "none";
				document.getElementById("imageLabel").style.display = "none";
				var dropDown = document.getElementById("carManufacturerIdPost").value;
				setCarManufacturerInDropdown(data.carManufacturer, dropDown);
				editingId = data.id;
			  formAction = "Update";
		  });
	  } else {
		  formAction = "Create";
		  console.log("Error occured with code " + response.status);
		  alert("Error occured!");
	  }
  })
  .catch(error => console.log(error));
}
function submitCarForm() {

  var carManufacturerId = document.getElementById("carManufacturerIdPost").value;
  var name = document.getElementById("name").value;
  var price = document.getElementById("price").value;
  var type = document.getElementById("type").value;
  var year = document.getElementById("ProduYear").value;
  var fuel = document.getElementById("fuelPostCar").value;
  var cubic = document.getElementById("cubicCapacity").value;
  var power = document.getElementById("power").value;
  var color = document.getElementById("color").value;
  var mileage = document.getElementById("mileage").value;
  var description = document.getElementById("description").value;
  var transmission = document.querySelector('input[name="Transmission"]:checked');
  var filesInput = document.getElementById("Images");

  	if (name === '' || price === ''|| year === '' || cubic === '' 
	|| power === '' || color === '' || mileage === '' || description === '' ) {
	  alert('Fill in the fields');
	  return false;
	}
  
  if (transmission) {
	  var selectedTransmission = transmission.value;
	  console.log("Selected transmission: " + selectedTransmission);
  } else {
	  console.log("No transmission selected");
  }
  var httpAction;
  var url;
  let formData = new FormData();

  if(formAction === "Create"){
	httpAction = "POST";
	url = host + port + CarEndpoint;

		formData.append("CarManufacturerId", carManufacturerId);
		formData.append("Price", price);
		formData.append("Name", name);
		formData.append("ProductionYear", year);
		formData.append("VehicleType", type);
		formData.append("Fuel", fuel);
		formData.append("Power", power);
		formData.append("CubicCapacity", cubic);
		formData.append("Mileage", mileage);
		formData.append("Color", color);
		formData.append("Description", description);
		formData.append("Transmission", selectedTransmission);
  
  for (var i = 0; i < filesInput.files.length; i++)
  {
  formData.append("Files", filesInput.files[i]);
	  }
	}else{
		httpAction = "PUT";
		url = host + port + CarEndpoint + editingId.toString();

		formData.append("id",editingId);
		formData.append("CarManufacturerId", carManufacturerId);
		formData.append("Price", price);
		formData.append("Name", name);
		formData.append("ProductionYear", year);
		formData.append("VehicleType", type);
		formData.append("Fuel", fuel);
		formData.append("Power", power);
		formData.append("CubicCapacity", cubic);
		formData.append("Mileage", mileage);
		formData.append("Color", color);
		formData.append("Description", description);
		formData.append("Transmission", selectedTransmission);
	}
   
  var headers = {};
  
  console.log("Objekat za slanje");
  console.log(formData);
  if (jwt_token) {
  headers.Authorization = 'Bearer ' + jwt_token;
  }
  fetch(url, { method: httpAction , headers: headers, body: formData })
  	.then((response) => {
  			if (response.status === 200 || response.status === 201) {
  			console.log("Successful action");
  			formAction = "Create";
			refreshTable();
  		} else {
  			console.log("Error occured with code " + response.status);
  			alert("Greska prilikom dodavanja!");
  }
  })
  .catch(error => console.log(error));
  return false;
}
function refreshTable() {
	document.getElementById("carForm").reset();
	loadPage();
};

function cancelCarForm() {
	formAction = "Create";
}

function submitSearchCarForm() {
    
	var carManufacturerId = document.getElementById("carManufacturerId").value;
	var minPrice = document.getElementById("minPrice").valueAsNumber; 
	var maxPrice = document.getElementById("maxPrice").valueAsNumber;
	var minYear = document.getElementById("minProductionYear").value;
	var maxYear = document.getElementById("maxProductionYear").value;
	var fuel = document.getElementById("fuelList").value;
	var minCubic = document.getElementById("minCubicCapacity").valueAsNumber;
	var maxCubic = document.getElementById("maxCubicCapacity").valueAsNumber;
	var minPower = document.getElementById("minPower").valueAsNumber;
	var maxPower = document.getElementById("maxPower").valueAsNumber;
	var minMileage = document.getElementById("minMileage").valueAsNumber;
	var maxMileage = document.getElementById("maxMileage").valueAsNumber;
	var manual = document.getElementById("manual").value;
	var automatic = document.getElementById("automatic").value;
	var transmission = document.querySelector('input[name="transmission"]:checked');

	if (transmission) {
  	  	var selectedTransmission = transmission.value;
   	 	console.log("Selected transmission: " + selectedTransmission);
	} else {
   		selectedTransmission = null;
	}

	var sendData = {
			"CarManufacturerId": carManufacturerId,
			"MinPrice": minPrice,
			"MaxPrice": maxPrice,
			"MinProductionYear": minYear,
			"MaxProductionYear" : maxYear,
			"Fuel" : fuel,
			"MinCubicCapacity": minCubic,
			"MaxCubicCapacity": maxCubic,
			"MinPower": minPower,
			"MaxPower": maxPower,
			"MinMileage": minMileage,
			"MaxMileage": maxMileage,
			"Transmission" : selectedTransmission
		};
	var url = host + port + filterCar;

	console.log("Objekat za slanje", sendData);
	console.log(sendData);

	fetch(url, { method: "POST", headers: { 'Content-Type': 'application/json' }, body: JSON.stringify(sendData) })
		.then((response) => {
			if (response.status === 200) {
				response.json().then(setCar);
				// document.getElementById("search-form").reset();
			} else {
				console.log("Error occured with code " + response.status);
				showError();
                alert("Greksa prilikom pretrage!");
			}
		})
		.catch(error => console.log(error));
	return false;
}
document.addEventListener("DOMContentLoaded", function () {
    var currentYear = new Date().getFullYear();
    var minProductionYearSelect = document.getElementById("minProductionYear");
    var maxProductionYearSelect = document.getElementById("maxProductionYear");

    // var emptyOption = document.createElement("option");
    // emptyOption.value = "";
    // emptyOption.text = "Select Year";
    // minProductionYearSelect.add(emptyOption.cloneNode(true));
    // maxProductionYearSelect.add(emptyOption.cloneNode(true));

    for (var year = currentYear; year >= 1940; year--) {
        var option = document.createElement("option");
        option.value = year;
        option.text = year;
        minProductionYearSelect.add(option.cloneNode(true));
        maxProductionYearSelect.add(option);
    }
});
document.addEventListener("DOMContentLoaded", function () {
    var currentYear = new Date().getFullYear();
    var minProductionYearSelect = document.getElementById("ProduYear");

    for (var year = currentYear; year >= 1940; year--) {
        var option = document.createElement("option");
        option.value = year;
        option.text = year;
        minProductionYearSelect.add(option);
    }
});
function previewImages() {
    var previewContainer = document.getElementById('imagePreview');
    previewContainer.innerHTML = '';

    var input = document.getElementById('Images');
    var files = input.files;

    for (var i = 0; i < files.length; i++) {

        var reader = new FileReader();

        reader.onload = function (e) {

            var img = document.createElement('img');
            img.src = e.target.result;
            img.classList.add('previewImage');
            previewContainer.appendChild(img);
        };

        reader.readAsDataURL(files[i]);
    }
}
