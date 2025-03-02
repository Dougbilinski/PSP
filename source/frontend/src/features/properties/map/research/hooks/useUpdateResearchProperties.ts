import { AxiosError, AxiosResponse } from 'axios';
import { useApiRequestWrapper } from 'hooks/pims-api/useApiRequestWrapper';
import { useApiResearchFile } from 'hooks/pims-api/useApiResearchFile';
import { IApiError } from 'interfaces/IApiError';
import { Api_ResearchFile } from 'models/api/ResearchFile';
import { useCallback } from 'react';
import { toast } from 'react-toastify';

/**
 * hook that updates a research file.
 */
export const useUpdateResearchProperties = () => {
  const { putResearchFileProperties } = useApiResearchFile();

  const { execute } = useApiRequestWrapper<
    (researchFile: Api_ResearchFile) => Promise<AxiosResponse<Api_ResearchFile, any>>
  >({
    requestFunction: useCallback(
      async (researchFile: Api_ResearchFile) => await putResearchFileProperties(researchFile),
      [putResearchFileProperties],
    ),
    requestName: 'UpdateResearchFileProperties',
    onSuccess: useCallback(() => toast.success('Research File Properties updated'), []),
    onError: useCallback((axiosError: AxiosError<IApiError>) => {
      if (axiosError?.response?.status === 400) {
        toast.error(axiosError?.response.data.error);
      } else {
        toast.error('Save error. Check responses and try again.');
      }
    }, []),
  });

  return { updateResearchFileProperties: execute };
};
