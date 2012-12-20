using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Text;
using ReactiveUI;
using RestSharp;

namespace Play
{
    public static class RestSharpRxHelper
    {
        public static IObservable<IRestResponse<T>> RequestAsync<T>(this IRestClient client, IRestRequest request) where T : new()
        {
            var ret = new AsyncSubject<IRestResponse<T>>();
            try {
                client.ExecuteAsync<T>(request, resp => {
                    ret.OnNext(resp);
                    ret.OnCompleted();
                });
            }
            catch (Exception ex) {
                ret.OnError(ex);
            }

            return ret.throwOnRestResponseFailure();
        }

        public static IObservable<IRestResponse> RequestAsync(this IRestClient client, IRestRequest request)
        {
            var ret = new AsyncSubject<IRestResponse>();
            try {
                client.ExecuteAsync(request, resp => {
                    ret.OnNext(resp);
                    ret.OnCompleted();
                });
            }
            catch (Exception ex) {
                ret.OnError(ex);
            }

            return ret.throwOnRestResponseFailure();
        }

        static IObservable<T> throwOnRestResponseFailure<T>(this IObservable<T> observable)
            where T : IRestResponse
        {
            return observable.SelectMany(x =>
            {
                if (x == null)
                {
                    return Observable.Return(x);
                }

                if (x.ErrorException != null)
                {
                    return Observable.Throw<T>(x.ErrorException);
                }

                if (x.ResponseStatus == ResponseStatus.Error)
                {
                    LogHost.Default.Warn("Response Status failed for {0}: {1}", x.ResponseUri, x.ResponseStatus);
                    return Observable.Throw<T>(new Exception("Request Error"));
                }

                if (x.ResponseStatus == ResponseStatus.TimedOut)
                {
                    LogHost.Default.Warn("Response Status failed for {0}: {1}", x.ResponseUri, x.ResponseStatus);
                    return Observable.Throw<T>(new Exception("Request Timed Out"));
                }

                if ((int)x.StatusCode >= 400)
                {
                    LogHost.Default.Warn("Response Status failed for {0}: {1}", x.ResponseUri, x.StatusCode);
                    return Observable.Throw<T>(new WebException(x.Content));
                }

                if (x.ResponseStatus == ResponseStatus.Aborted)
                {
                    LogHost.Default.Warn("Response Status failed for {0}: {1}", x.ResponseUri, x.ResponseStatus);
                    return Observable.Throw<T>(new Exception("Request aborted"));
                }

                return Observable.Return(x);
            });
        }
    }
}