using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Security.Cryptography;
using Windows.Security.Cryptography.DataProtection;
using Windows.Storage.Streams;
using System.Threading.Tasks;
using Windows.Storage;

namespace BudgetManager.Core
{
    public  class Cryptology
    {
        

        public async void DoEncrypt()
        {
            var storagePath = Path.Combine(ApplicationData.Current.LocalFolder.Path, "BudgetManagerDB.db");
            StorageFile FileForEncryption = await StorageFile.GetFileFromPathAsync(storagePath);
            IBuffer data = await FileIO.ReadBufferAsync(FileForEncryption);
            IBuffer SecuredData = await SampleDataProtectionStream("LOCAL = user", data);
            StorageFile EncryptedFile = await ApplicationData.Current.LocalFolder.CreateFileAsync(
              "BudgetManagerDB" + FileForEncryption.FileType, CreationCollisionOption.ReplaceExisting);
            StorageFile tempEncryptedFile = await ApplicationData.Current.LocalFolder.CreateFileAsync(
            "temp" + FileForEncryption.FileType, CreationCollisionOption.ReplaceExisting);
            await FileIO.WriteBufferAsync(EncryptedFile, SecuredData);
            await FileIO.WriteBufferAsync(tempEncryptedFile, SecuredData);
        }

        public async void DoDecrypt()
        {
            var storagePathForMainFile = Path.Combine(ApplicationData.Current.LocalFolder.Path, "BudgetManagerDB.db");
            StorageFile FileForEncryption = await StorageFile.GetFileFromPathAsync(storagePathForMainFile);
            bool b = true;
            try
            {
                var item = await ApplicationData.Current.LocalFolder.GetFileAsync("temp.db");
               
            }
            catch (FileNotFoundException ex)
            {
                b = false;
            }

            if (b)
            {
                IBuffer data = await FileIO.ReadBufferAsync(FileForEncryption);
                IBuffer UnSecuredData = await SampleDataUnprotectStream(data);
                StorageFile DecryptedFile = await ApplicationData.Current.LocalFolder.CreateFileAsync(
                  "BudgetManagerDB" + FileForEncryption.FileType, CreationCollisionOption.ReplaceExisting);
                await FileIO.WriteBufferAsync(DecryptedFile, UnSecuredData);
            }

        }

        public async Task<IBuffer> SampleDataProtectionStream(String descriptor, IBuffer buffMsg)
        {
            // Create a DataProtectionProvider object for the specified descriptor.
            DataProtectionProvider Provider = new DataProtectionProvider(descriptor);

            // Create a random access stream to contain the plaintext message.
            InMemoryRandomAccessStream inputData = new InMemoryRandomAccessStream();

            // Create a random access stream to contain the encrypted message.
            InMemoryRandomAccessStream protectedData = new InMemoryRandomAccessStream();

            // Retrieve an IOutputStream object and fill it with the input (plaintext) data.
            IOutputStream outputStream = inputData.GetOutputStreamAt(0);
            DataWriter writer = new DataWriter(outputStream);
            writer.WriteBuffer(buffMsg);
            await writer.StoreAsync();
            await outputStream.FlushAsync();

            // Retrieve an IInputStream object from which you can read the input data.
            IInputStream source = inputData.GetInputStreamAt(0);

            // Retrieve an IOutputStream object and fill it with encrypted data.
            IOutputStream dest = protectedData.GetOutputStreamAt(0);
            await Provider.ProtectStreamAsync(source, dest);
            await dest.FlushAsync();

            //Verify that the protected data does not match the original
            DataReader reader1 = new DataReader(inputData.GetInputStreamAt(0));
            DataReader reader2 = new DataReader(protectedData.GetInputStreamAt(0));
            await reader1.LoadAsync((uint)inputData.Size);
            await reader2.LoadAsync((uint)protectedData.Size);
            IBuffer buffOriginalData = reader1.ReadBuffer((uint)inputData.Size);
            IBuffer buffProtectedData = reader2.ReadBuffer((uint)protectedData.Size);

            if (CryptographicBuffer.Compare(buffOriginalData, buffProtectedData))
            {
                throw new Exception("ProtectStreamAsync returned unprotected data");
            }

            // Return the encrypted data.
            return buffProtectedData;
        }

        public async Task<IBuffer> SampleDataUnprotectStream(IBuffer buffProtected)
        {
            // Create a DataProtectionProvider object.
            DataProtectionProvider Provider = new DataProtectionProvider();

            // Create a random access stream to contain the encrypted message.
            InMemoryRandomAccessStream inputData = new InMemoryRandomAccessStream();

            // Create a random access stream to contain the decrypted data.
            InMemoryRandomAccessStream unprotectedData = new InMemoryRandomAccessStream();

            // Retrieve an IOutputStream object and fill it with the input (encrypted) data.
            IOutputStream outputStream = inputData.GetOutputStreamAt(0);
            DataWriter writer = new DataWriter(outputStream);
            writer.WriteBuffer(buffProtected);
            await writer.StoreAsync();
            await outputStream.FlushAsync();

            // Retrieve an IInputStream object from which you can read the input (encrypted) data.
            IInputStream source = inputData.GetInputStreamAt(0);

            // Retrieve an IOutputStream object and fill it with decrypted data.
            IOutputStream dest = unprotectedData.GetOutputStreamAt(0);
            await Provider.UnprotectStreamAsync(source, dest);
            await dest.FlushAsync();

            // Write the decrypted data to an IBuffer object.
            DataReader reader2 = new DataReader(unprotectedData.GetInputStreamAt(0));
            await reader2.LoadAsync((uint)unprotectedData.Size);
            IBuffer buffUnprotectedData = reader2.ReadBuffer((uint)unprotectedData.Size);

            // Return the decrypted data.
            return buffUnprotectedData;
        }


    }
}
