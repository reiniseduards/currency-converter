// Function to handle the submission of the exchange form
function submitExchangeForm() {
    // Get values from form inputs
    const amount = $('#amount').val();
    const fromCurrency = $('#fromCurrency').val();
    const toCurrency = $('#toCurrency').val();

    // Validate the input values
    if (!validateAmount(amount, fromCurrency, toCurrency)) {
        renderError("Please enter valid input");
        return;
    }

    // Create a data object with the form values
    const formData = { amount, fromCurrency, toCurrency };

    // Make an AJAX request with the form data
    makeAjaxRequest(formData);
}

// Function to validate the amount input
function validateAmount(amount) {
    var pattern = /^\d+(\.\d{1,2})?$/;
    // Regular expression to validate amount format
    // It allows 1 or more digits, optionally followed by a decimal point and 1 or 2 digits
    return pattern.test(amount);
}

// Function to make an AJAX request
function makeAjaxRequest(formData) {
    $.ajax({
        url: '/Home/TryConvert',
        method: 'post',
        data: formData,
        success: function (data) {
            // Handle success by calling the appropriate function
            handleAjaxSuccess(data);
        },
        error: function () {
            // Handle error by calling the appropriate function
            handleAjaxError("An error occurred submitting the input");
        }
    });
}

// Function to handle AJAX success
function handleAjaxSuccess(data) {
    if (data.result === "success") {
        // If the result is success, create a model object and render the view
        const model = {
            amount: data.amount,
            base_name: data.base_name,
            base_symbol: data.base_symbol,
            target_name: data.target_name,
            target_symbol: data.target_symbol,
            conversion_result: data.conversion_result,
            conversion_rate: data.conversion_rate
        };

        // Update the result view with the rendered view
        $('#result-view').html(renderView(model));
    } else {
        // If the result is not success, handle the error
        handleAjaxError("An error occurred during the calculation");
    }
}

// Function to handle AJAX errors
function handleAjaxError(message) {
    // Render an error message in the result view
    renderError(message);
}

// Function to render the view based on the model
function renderView(model) {
    return `
        <p>${model.amount} ${model.base_name} = ${model.conversion_result} ${model.target_name}</p>
        <p style="font-size: 0.75em">1${model.base_symbol} = ${model.conversion_rate}${model.target_symbol}</p>
    `;
}

// Function to render an error message in the result view
function renderError(message) {
    const view = `<p style="color: red">${message}</p>`;
    $('#result-view').html(view);
}
