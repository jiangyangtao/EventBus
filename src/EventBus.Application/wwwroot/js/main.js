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



var api = {
    application: {
        add: function (data) {
            return axios.post('/api/application/add', data);
        },
        delete: function (applicationId) {
            return axios.delete(`/api/application/delete/${applicationId}`);
        },
        modify: function (data) {
            return axios.put('/api/application/modify', data);
        },
        get: function (applicationId) {
            return axios.get(`/api/application/Get/${applicationId}`);
        },
        list: function (start, count, applicationName) {
            return axios.get('/api/application/list', {
                params: {
                    startIndex: start,
                    count: count,
                    ApplicationName: applicationName
                }
            });
        }
    },
    applicationEndpoint: {
        add: function (data) {
            return axios.post('/api/applicationendpoint/add', data);
        },
        delete: function (applicationEndpointId) {
            return axios.delete(`/api/applicationendpoint/delete/${applicationEndpointId}`);
        },
        modify: function (data) {
            return axios.put('/api/applicationendpoint/modify', data);
        },
        get: function (applicationEndpointId) {
            return axios.get(`/api/applicationendpoint/Get/${applicationEndpointId}`);
        },
        list: function (start, count, endpointName) {
            return axios.get('/api/applicationendpoint/list', {
                params: {
                    startIndex: start,
                    count: count,
                    EndpointName: endpointName
                }
            });
        }
    },
    event: {
        add: function (data) {
            return axios.post('/api/event/add', data);
        },
        delete: function (eventId) {
            return axios.delete(`/api/event/delete/${eventId}`);
        },
        modify: function (data) {
            return axios.put(`/api/event/modify/${data.EventId}`, data);
        },
        get: function (eventId) {
            return axios.get(`/api/event/Get/${eventId}`);
        },
        list: function (start, count, eventName) {
            return axios.get('/api/event/list', {
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
            return axios.get('/api/eventrecord/list', {
                params: {
                    startIndex: start,
                    count: count,
                    EventName: eventName
                }
            });
        },
        getSubscriptionRecord: function (eventRecordId) {
            return axios.get(`/api/eventrecord/getSubscriptionRecord/${eventRecordId}`);
        },
        getEndpointSubscriptionRecord: function (subscriptionRecordId) {
            return axios.get(`/api/eventrecord/getEndpointSubscriptionRecord/${subscriptionRecordId}`);
        }
    },
    retry: {
        modify: function (retryDataId) {
            return axios.put(`/api/retry/retry/${retryDataId}`);
        },
        list: function (start, count, eventName, endpointName) {
            return axios.get('/api/event/list', {
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
            return axios.post('/api/subscription/add', data);
        },
        delete: function (subscriptionId) {
            return axios.delete(`/api/subscription/delete/${subscriptionId}`);
        },
        get: function (subscriptionId) {
            return axios.get(`/api/subscription/Get/${subscriptionId}`);
        },
        list: function (start, count, eventId, endpointName) {
            return axios.get('/api/subscription/list', {
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
