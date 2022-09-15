// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.


Promise.prototype.success = Promise.prototype.then;
Promise.prototype.fail = Promise.prototype.catch;


axios.interceptors.response.use(function (response) {
    if (response.status >= 200 && response.status <= 300) return response.data;

    return Promise.reject({
        message: response.data.Message
    });
}, function (error) {
    return Promise.reject({
        message: error.response.data.Message
    });
});



const apiVersion = "v1";
var api = {
    application: {
        add: function (data) {
            return axios.post(`${apiVersion}/api/application/add`, data);
        },
        delete: function (applicationId) {
            return axios.delete(`${apiVersion}/api/application/delete/${applicationId}`);
        },
        modify: function (data) {
            return axios.put(`${apiVersion}/api/application/modify/${data.ApplicationId}`, data);
        },
        get: function (applicationId) {
            return axios.get(`${apiVersion}/api/application/Get/${applicationId}`);
        },
        list: function (start, count, applicationName) {
            return axios.get(`${apiVersion}/api/application/list`, {
                params: {
                    startIndex: start,
                    count: count,
                    ApplicationName: applicationName
                }
            });
        },
        suggestion: function ( applicationName) {
            return axios.get(`${apiVersion}/api/application/suggestion`, {
                params: {
                    ApplicationName: applicationName
                }
            });
        },
    },
    applicationEndpoint: {
        add: function (data) {
            return axios.post(`${apiVersion}/api/applicationendpoint/add`, data);
        },
        delete: function (applicationEndpointId) {
            return axios.delete(`${apiVersion}/api/applicationendpoint/delete/${applicationEndpointId}`);
        },
        modify: function (data) {
            return axios.put(`${apiVersion}/api/applicationendpoint/modify/${data.ApplicationEndpointId}`, data);
        },
        get: function (applicationEndpointId) {
            return axios.get(`${apiVersion}/api/applicationendpoint/Get/${applicationEndpointId}`);
        },
        list: function (start, count, endpointName, applicationId) {
            return axios.get(`${apiVersion}/api/applicationendpoint/list`, {
                params: {
                    startIndex: start,
                    count: count,
                    EndpointName: endpointName,
                    ApplicationId: applicationId
                }
            });
        }
    },
    event: {
        add: function (data) {
            return axios.post(`${apiVersion}/api/event/add`, data);
        },
        delete: function (eventId) {
            return axios.delete(`${apiVersion}/api/event/delete/${eventId}`);
        },
        modify: function (data) {
            return axios.put(`${apiVersion}/api/event/modify/${data.EventId}`, data);
        },
        get: function (eventId) {
            return axios.get(`${apiVersion}/api/event/Get/${eventId}`);
        },
        list: function (start, count, eventName) {
            return axios.get(`${apiVersion}/api/event/list`, {
                params: {
                    startIndex: start,
                    count: count,
                    EventName: eventName
                }
            });
        }
    },
    eventRecord: {
        list: function (start, count, eventName) {
            return axios.get(`${apiVersion}/api/eventrecord/list`, {
                params: {
                    startIndex: start,
                    count: count,
                    EventName: eventName
                }
            });
        },
        getSubscriptionRecord: function (eventRecordId) {
            return axios.get(`${apiVersion}/api/eventrecord/getSubscriptionRecord/${eventRecordId}`);
        },
        getEndpointSubscriptionRecord: function (subscriptionRecordId) {
            return axios.get(`${apiVersion}/api/eventrecord/getEndpointSubscriptionRecord/${subscriptionRecordId}`);
        }
    },
    retry: {
        modify: function (retryDataId) {
            return axios.put(`${apiVersion}/api/retry/retry/${retryDataId}`);
        },
        list: function (start, count, eventName, endpointName) {
            return axios.get(`${apiVersion}/api/event/list`, {
                params: {
                    startIndex: start,
                    count: count,
                    EventName: eventName,
                    EndpointName: endpointName,
                }
            });
        }
    },
    subscription: {
        add: function (data) {
            return axios.post(`${apiVersion}/api/subscription/add`, data);
        },
        addSubscription: function (data) {
            return axios.post(`${apiVersion}/api/subscription/addsubscription`, data);
        },
        modify: function (data) {
            return axios.put(`${apiVersion}/api/subscription/modify/${data.SubscriptionId}`, data);
        },
        delete: function (subscriptionId) {
            return axios.delete(`${apiVersion}/api/subscription/delete/${subscriptionId}`);
        },
        get: function (subscriptionId) {
            return axios.get(`${apiVersion}/api/subscription/Get/${subscriptionId}`);
        },
        list: function (start, count, eventId, endpointName) {
            return axios.get(`${apiVersion}/api/subscription/list`, {
                params: {
                    startIndex: start,
                    count: count,
                    EndpointName: endpointName,
                    EventId: eventId
                }
            });
        }
    }
};
